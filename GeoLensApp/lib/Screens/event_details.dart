import 'package:carousel_pro/carousel_pro.dart';
import 'package:data_discovery_for_earth/Models/event_image_model.dart';
import 'package:data_discovery_for_earth/Models/event_info_model.dart';
import 'package:data_discovery_for_earth/Models/event_refrence_model.dart';
import 'package:data_discovery_for_earth/Services/database_helper.dart';
import 'package:data_discovery_for_earth/Services/network_util.dart';
import 'package:eva_icons_flutter/eva_icons_flutter.dart';
import 'package:flutter/material.dart';
import 'package:network_image_to_byte/network_image_to_byte.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:intl/intl.dart';
import 'package:intl/intl.dart' as intl;

import '../localizations.dart';
import '../theme.dart';

class EventDetails extends StatefulWidget {
  final EventInfoModel _eventInfoModel;
  final String _lang;
  EventDetails(this._eventInfoModel, this._lang);

  @override
  _EventDetailsState createState() =>
      _EventDetailsState(_eventInfoModel, _lang);
}

class _EventDetailsState extends State<EventDetails> {
  final EventInfoModel _eventInfoModel;
  final String _lang;
  List<EventRefrenceModel> _data = [];
  List<EventImageModel> _images = [];
  _EventDetailsState(this._eventInfoModel, this._lang);

  Future<List<EventRefrenceModel>> _GetEventRefrence(String eventID) async {
    List<EventRefrenceModel> result = [];
    final query = "AEventRefrences?eventID=$eventID";

    var resultJson = await NetworkUtil.internal().get(query);
    if (resultJson != false) {
      for (var item in resultJson) {
        result.add(EventRefrenceModel.fromJson(item));
      }
    }

    return result;
  }

  Future<List<EventImageModel>> _GetEventImages(String eventID) async {
    List<EventImageModel> result = [];
    final query = "AEventImage?eventID=$eventID";

    var resultJson = await NetworkUtil.internal().get(query);
    if (resultJson != false) {
      for (var item in resultJson) {
        result.add(EventImageModel.fromJson(item));
      }
    }

    return result;
  }

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    print(_eventInfoModel.isFav);
    _GetEventRefrence(_eventInfoModel.eventID.toString()).then((value) {
      setState(()  {
        _data = value;
      });
    });

    _GetEventImages(_eventInfoModel.eventID.toString()).then((value) {
      setState(() {
        _images = value;
      });
    });
  }

  var db = new DatabaseHelper();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: OwnColors.color2[50],
      appBar: AppBar(
        title: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Padding(
              padding: const EdgeInsets.only(top: 20.0, bottom: 20),
              child: Text(
                DateFormat("d-M-yyyy", "en_US")
                    .format(DateTime.parse(_eventInfoModel.eventDate)),
                style: OwnColors.bigWhite(),
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(top: 20.0, bottom: 20),
              child: InkWell(
                  onTap: () {
                    if (_eventInfoModel.isFav) {
                      _deleteDb(widget._eventInfoModel);
                    } else {
                      _saveDb(widget._eventInfoModel);
                    }
                  },
                  child: Icon(
                    _eventInfoModel.isFav
                        ? EvaIcons.star
                        : EvaIcons.starOutline,
                    color: Colors.white,
                  )),
            ),
          ],
        ),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.only(right: 8, left: 8),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              ListTile(
                contentPadding: EdgeInsets.all(2),
                dense: true,
                title: Text(
                  widget._lang == "ar"
                      ? widget._eventInfoModel.eventNameAr
                      : widget._eventInfoModel.eventNameEn,
                  style: TextStyle(
                      color: OwnColors.color2[500],
                      fontWeight: FontWeight.w500,
                      fontSize: 15),
                ),
                leading: Padding(
                  padding: const EdgeInsets.all(0.0),
                  child: Container(
                    decoration: BoxDecoration(
                        color: OwnColors.color1[500],
                        borderRadius: BorderRadius.circular(40.0)),
                    child: Padding(
                      padding: const EdgeInsets.all(3.0),
                      child: ClipOval(
                          child: _eventInfoModel != null
                              ? FutureBuilder<Widget>(
                                  future: getImageLogo3(
                                      _eventInfoModel.eventTypeID.toString()),
                                  builder: (BuildContext context,
                                      AsyncSnapshot<Widget> snapshot) {
                                    if (snapshot.hasData) return snapshot.data;

                                    return Icon(EvaIcons.alertTriangleOutline);
                                  })
                              : Container()),
                    ),
                  ),
                ),
              ),
              SizedBox(
                height: 5,
              ),
              Container(
                decoration: BoxDecoration(
                    border: Border.all(width: 0.1, color: Colors.grey)),
                child: _images.length > 0
                    ? SizedBox(
                        height: MediaQuery.of(context).size.width - 190,
                        width: MediaQuery.of(context).size.width,
                        child: Carousel(
                          images: _images.map((f) {
                            return _getImage(
                                context, f.eventImageID.toString());
                          }).toList(),
                          dotSize: 7.0,
                          dotSpacing: 15.0,
                          dotColor: Colors.red,
                          indicatorBgPadding: 5.0,
                          dotBgColor: OwnColors.color2[500],
                          radius: Radius.circular(1.0),
                          borderRadius: true,
                          autoplay: false,

//
                        ))
                    : Container(),
              ),
              SizedBox(
                height: 5,
              ),
              Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Text(
                    widget._lang == "ar"
                        ? widget._eventInfoModel.eventDescAr
                        : widget._eventInfoModel.eventDescEn,
                    style: TextStyle(height: 1.5),
                    textAlign: _lang =='ar' ? TextAlign.right :TextAlign.justify,
                  )),
              SizedBox(
                height: 5,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [],
              ),
              Divider(),
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: Text(
                  AppLocalizations.of(context).refrences_label,
                  style: TextStyle(
                      color: OwnColors.color2[500],
                      fontWeight: FontWeight.w500,
                      fontSize: 15),
                ),
              ),
              Column(
                  children: _data != null
                      ? List.generate(_data.length, (index) {
                          return Padding(
                            padding: const EdgeInsets.all(5.0),
                            child: InkWell(
                                onTap: () {
                                  _launchURL(_data[index].eventURL);
                                },
                                child: Padding(
                                  padding: const EdgeInsets.all(8.0),
                                  child: Container(
                                    alignment: Alignment.centerLeft,
                                    child: Text(
                                      _data[index].eventURL,
                                      textAlign: TextAlign.left,
                                    ),
                                  ),
                                )),
                          );
                        }).toList()
                      : Container())
            ],
          ),
        ),
      ),
    );
  }

  Widget _getImage(BuildContext context, String mID) {
    var base = NetworkUtil.internal().imageUrl;
    String rr = base + "EventImage/IE$mID.png";
    return Image.network(
      rr,
      width: MediaQuery.of(context).size.width - 15,
      fit: BoxFit.contain,
    );
  }

  _launchURL(String url) async {
    if (await canLaunch(url)) {
      await launch(url);
    } else {
      throw 'Could not launch $url';
    }
  }

  Future<Image> getImageLogo3(String mID) async {
    var base = NetworkUtil.internal().imageUrl;
    String rr = base + "EventType/ET${mID}.png";
    final _logo = await networkImageToByte(rr);
    return Image.memory(
      _logo,
      width: 25,
      height: 25,
      color: OwnColors.color2[500],
      fit: BoxFit.fill,
    );
  }

  _deleteDb(EventInfoModel obj) {
    setState(() {
      widget._eventInfoModel.isFav = false;
    });

    db.delete(obj.eventID);
  }

  _saveDb(EventInfoModel obj) async {
    db.savePost(obj);

    setState(() {
      widget._eventInfoModel.isFav = true;
    });
  }
}
