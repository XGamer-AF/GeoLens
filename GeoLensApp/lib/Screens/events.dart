import 'package:data_discovery_for_earth/Models/event_details_model.dart';
import 'package:data_discovery_for_earth/Models/event_info_model.dart';
import 'package:data_discovery_for_earth/Models/event_types_model.dart';
import 'package:data_discovery_for_earth/Screens/event_details.dart';
import 'package:data_discovery_for_earth/Services/data_loading_util.dart';
import 'package:data_discovery_for_earth/Services/database_helper.dart';
import 'package:data_discovery_for_earth/Services/network_util.dart';
import 'package:data_discovery_for_earth/theme.dart';
import 'package:eva_icons_flutter/eva_icons_flutter.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:network_image_to_byte/network_image_to_byte.dart';
import 'package:intl/intl.dart';

import '../localizations.dart';
import 'CookiesClass.dart';

class Events extends StatefulWidget {
  @override
  _EventsState createState() => _EventsState();
}

class _EventsState extends State<Events> with SingleTickerProviderStateMixin {
  TabController _controller;
  int _selectedIndex = 0;
  String _eventTypeID = '';
  String _lang = "";
  List<EventInfoModel> result = [];
  List<EventInfoModel> _data = [];
  List<EventTypesModel> _EventType = [];
  TextEditingController controller = new TextEditingController();
  bool isSearch = false;
  dynamic connectionStatus = null;
/////////////////////////////////////
  var db = new DatabaseHelper();

  Future<dynamic> _GetEvent(String number, String eventTypeID, String countryID,
      String dateFrom, String dateTo) async {
    List<EventInfoModel> result = [];
    final query =
        "AEventInfo?number=$number&eventTypeID=$_eventTypeID&countryID=$countryID&dateFrom=$dateFrom&dateTo=$dateTo";
    var resultJson = await NetworkUtil.internal().get(query);
    if (resultJson == false || resultJson == 'noConnection') {
      return resultJson;
    } else {
      for (var item in resultJson) {
        result.add(EventInfoModel.fromJson(item));
      }

      return result;
    }
  }

  _GetEventType() async {
    List<EventTypesModel> result = [];
    final query = "AEventTypes";
    var resultJson = await NetworkUtil.internal().get(query);
    if (resultJson == false || resultJson == 'noConnection') {
      setState(() {
        _EventType = [];
      });
    } else {
      for (var item in resultJson) {
        result.add(EventTypesModel.fromJson(item));
      }

      setState(() {
        _EventType = result;
      });
      var uu = new EventTypesModel();
      uu.eventTypeID = -1;
      uu.eventTypeNameAr = "All";
      uu.eventTypeNameEn = "الكل";
      uu.statusID = 1;
      result.insert(0, uu);
    }
  }

  _updateData(String number, String eventTypeID, String countryID,
      String dateFrom, String dateTo) {
    _GetEvent(number, eventTypeID, countryID, dateFrom, dateTo)
        .then((value) async {
      if (value == false || value == 'noConnection') {
        setState(() {
          connectionStatus = value;
        });
      } else {
        setState(() {
          connectionStatus = "true";
          result = value;
        });
        List<EventInfoModel> Fav = await db.getAll();

        if (result.length > 0) {
          if (Fav.length > 0) {
            for (var row in Fav) {
              var ii = result
                  .where((element) => element.eventID == row.eventID)
                  .first;
              if (ii != null) {
                ii.isFav = true;
              }
            }
          }
        }

        setState(() {
          _data = result;
          _textSearch(controller.text);
        });
      }
    });
  }

  Future<String> getLang() async {
    return await getValuesSF("lang");
  }

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    getLang().then((value) {
      setState(() {
        _lang = value;
      });
    });
    _controller = new TabController(length: 2, vsync: this);
    setState(() {
      _controller.index = 0;
    });
    _GetEventType();
    _updateData("10", "", "", "", "");
  }

  @override
  void dispose() {
    // TODO: implement dispose
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: Colors.white,
        appBar: AppBar(
          actionsIconTheme: IconThemeData(size: 10),
          actions: <Widget>[
            Row(
              children: [
                IconButton(
                    icon: isSearch
                        ? Icon(EvaIcons.closeOutline)
                        : Icon(EvaIcons.searchOutline),
                    onPressed: () {
                      setState(() {
                        isSearch = !isSearch;
                        controller.clear();
                        _textSearch(controller.text);
                      });
                    }),
                Padding(
                  padding: const EdgeInsets.only(top: 0, left: 0, right: 0),
                  child: PopupMenuButton(
                    onSelected: (val) {
                      setState(() {
                        controller.clear();
                      });
                      if (val == -1) {
                        setState(() {
                          _eventTypeID = "";
                          result = _data;
                        });
                      } else {
                        var tt = _data.where((element) {
                          return element.eventTypeID == val;
                        }).toList();
                        setState(() {
                          _eventTypeID = val.toString();
                          result = tt;
                        });
                      }
                    },
                    icon: Icon(EvaIcons.funnelOutline),
                    itemBuilder: (BuildContext context) {
                      return _EventType.map((item) {
                        return PopupMenuItem(
                          value: item.eventTypeID,
                          child: new ListTile(
                            dense: true,
                            contentPadding: EdgeInsets.only(right: 5, left: 5),
                            leading: Container(
                              decoration: BoxDecoration(
                                  color: OwnColors.color1[500],
                                  borderRadius: BorderRadius.circular(40.0)),
                              child: Padding(
                                padding: const EdgeInsets.all(3.0),
                                child: ClipOval(
                                    child: item != 0
                                        ? FutureBuilder<Widget>(
                                            future: getImageLogo3(
                                                item.eventTypeID.toString()),
                                            builder: (BuildContext context,
                                                AsyncSnapshot<Widget>
                                                    snapshot) {
                                              if (snapshot.hasData &&
                                                  snapshot.data != null)
                                                return snapshot.data;
                                              return Icon(
                                                EvaIcons.alertTriangleOutline,
                                              );
                                            })
                                        : Container()),
                              ),
                            ),
                            title: _lang == 'en'
                                ? Text(item.eventTypeNameAr)
                                : Text(item.eventTypeNameEn),
                          ),
                          // value: item.eventTypeID,
                          // child: Row(
                          //   mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          //   children: <Widget>[
                          //     Text(
                          //       _lang == "ar"
                          //           ? item.eventTypeNameEn
                          //           : item.eventTypeNameAr,
                          //       style: OwnColors.normalBlack(),
                          //     ),
                          //   ],
                          // ),
                        );
                      }).toList();
                    },
                  ),
                ),
              ],
            ),
          ],
          title: isSearch == true
              ? Padding(
                  padding: const EdgeInsets.only(top: 0),
                  child: TextField(
                      style: OwnColors.normalWhite(),
                      controller: controller,
                      decoration: new InputDecoration(
                        contentPadding: EdgeInsets.only(top: 15),
                        prefixIcon: new Icon(
                          Icons.search,
                          color: Colors.white,
                        ),
                        hintText: AppLocalizations.of(context).search_label,
                        hintStyle: TextStyle(color: Colors.white),
                      ),
                      onChanged: (val) {
                        _textSearch(val);
                      }),
                )
              : Text(
                  AppLocalizations.of(context).events_title,
                ),
          bottom: PreferredSize(
            child: Container(
              color: OwnColors.color1[500],
              height: 35.0,
              child: TabBar(
                indicatorColor: Colors.blueGrey,
                labelColor: OwnColors.color2[500],
                controller: _controller,
                tabs: [
                  Tab(
                    text: AppLocalizations.of(context).latestEvents,
                  ),
                  Tab(
                    text: AppLocalizations.of(context).allEvents,
                  )
                ],
                onTap: (index) {
                  if (index == 0) {
                    setState(() {
                      _selectedIndex = index;
                      _updateData("10", "", "", "", "");
                    });
                  } else {
                    setState(() {
                      _selectedIndex = index;
                      _updateData("all", "", "", "", "");
                    });
                  }
                },
              ),
            ),
            preferredSize: Size.fromHeight(20.0),
          ),
        ),
        body: connectionStatus == null
            ? DataLoadingUtil().LoadingData()
            : connectionStatus == "noConnection"
                ? DataLoadingUtil().NoConnection(context)
                : connectionStatus == false
                    ? DataLoadingUtil().NoData(context)
                    : ListView.builder(
                        itemCount: result != null ? result.length : 0,
                        itemBuilder: (BuildContext context, int index) {
                          return _listItem(index);
                        }));
  }

  _listItem(index) {
    return InkWell(
      onTap: () {
        Navigator.of(context).push(MaterialPageRoute(
            builder: (BuildContext context) =>
                EventDetails(result[index], _lang)));
      },
      child: Padding(
        padding: EdgeInsets.only(right: 10, left: 10, top: 2, bottom: 2),
        child: Container(
          color: Colors.white,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              SizedBox(
                height: 5,
              ),
              Row(
                children: [
                  Padding(
                    padding: const EdgeInsets.all(3.0),
                    child: Container(
                      decoration: BoxDecoration(
                          color: OwnColors.color1[500],
                          borderRadius: BorderRadius.circular(40.0)),
                      child: Padding(
                        padding: const EdgeInsets.all(3.0),
                        child: ClipOval(
                            child: result.length > 0
                                ? FutureBuilder<Widget>(
                                    future: getImageLogo3(
                                        result[index].eventTypeID.toString()),
                                    builder: (BuildContext context,
                                        AsyncSnapshot<Widget> snapshot) {
                                      if (snapshot.hasData)
                                        return snapshot.data;

                                      return Icon(
                                          EvaIcons.alertTriangleOutline);
                                    })
                                : Container()),
                      ),
                    ),
                  ),
                  Padding(
                    padding: const EdgeInsets.all(3.0),
                    child: Text(
                        _lang == "ar"
                            ? result[index].eventNameAr
                            : result[index].eventNameEn,
                        style: TextStyle(
                          color: OwnColors.color2[500],
                          fontWeight: FontWeight.w500,
                        )),
                  ),
                ],
              ),
              SizedBox(
                height: 5,
              ),
              (result[index].eventImage == null ||
                      result[index].eventImage == "")
                  ? Container()
                  : _getImage(result[index].eventImage.toString()),
              SizedBox(
                height: 5,
              ),
              Text(
                _lang == "ar"
                    ? (result[index].eventDescAr.toString() != null)
                        ? result[index].eventDescAr.toString().length > 200
                            ? result[index]
                                .eventDescAr
                                .toString()
                                .substring(0, 200)
                            : result[index].eventDescAr.toString()
                        : Container()
                    : (result[index].eventDescEn.toString() != null)
                        ? result[index].eventDescEn.toString().length > 200
                            ? result[index]
                                .eventDescEn
                                .toString()
                                .substring(0, 200)
                            : result[index].eventDescEn.toString()
                        : Container(),
                style: TextStyle(color: Colors.black, height: 1.5),
                textAlign: _lang == 'ar' ? TextAlign.right : TextAlign.justify,
              ),
              SizedBox(
                height: 5,
              ),
              Container(
                color: OwnColors.color2[500],
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      InkWell(
                          onTap: () {
                            var obj = result[index];
                            if (result[index].isFav) {
                              _deleteDb(obj, index);
                            } else {
                              _saveDb(obj, index);
                            }
                          },
                          child: Icon(
                            result[index].isFav
                                ? EvaIcons.star
                                : EvaIcons.starOutline,
                            color: Colors.white,
                          )),
                      Text(
                        DateFormat("d-M-yyyy", "en_US")
                            .format(DateTime.parse(result[index].eventDate)),
                        style: OwnColors.bigWhite(),
                      )
                    ],
                  ),
                ),
              ),
              SizedBox(
                height: 5,
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _getImage(String mID) {
    var base = NetworkUtil.internal().imageUrl;
    String rr = base + "EventImage/$mID";
    return Image.network(
      rr,
      width: MediaQuery.of(context).size.width,
      fit: BoxFit.fitHeight,
    );
  }

  _textSearch(val) {
    val = val.toLowerCase();
    setState(() {
      result = _data.where((row) {
        var r1 = row.eventNameAr.toString().toLowerCase();
        var r2 = row.eventNameEn.toString().toLowerCase();
        // var r3 = row..toString().toLowerCase();

        return r1.contains(val) || r2.contains(val);
      }).toList();
    });
  }

  _saveDb(EventInfoModel obj, index) async {
    db.savePost(obj);

    setState(() {
      result[index].isFav = true;
    });
  }

  _deleteDb(EventInfoModel obj, index) {
    setState(() {
      result[index].isFav = false;
    });

    db.delete(obj.eventID);
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
}
