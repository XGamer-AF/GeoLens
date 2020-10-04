import 'package:data_discovery_for_earth/Models/event_info_model.dart';
import 'package:data_discovery_for_earth/Services/data_loading_util.dart';
import 'package:data_discovery_for_earth/Services/database_helper.dart';
import 'package:data_discovery_for_earth/Services/network_util.dart';
import 'package:data_discovery_for_earth/localizations.dart';
import 'package:eva_icons_flutter/eva_icons_flutter.dart';
import 'package:flutter/material.dart';
import 'package:network_image_to_byte/network_image_to_byte.dart';

import '../theme.dart';
import 'CookiesClass.dart';
import 'event_details.dart';

class EventFav extends StatefulWidget {
  @override
  _EventFavState createState() => _EventFavState();
}

class _EventFavState extends State<EventFav> {
  String _lang="";
  var db = new DatabaseHelper();

  List<EventInfoModel> _data = [];


  Future<String> getLang() async {
    return await getValuesSF("lang");
  }
  Future<void> _GetEvent() async {
    List<EventInfoModel> result = [];

       result =await db.getAll();

     setState(() {
       _data= result;
     });

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
    _GetEvent();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: Text(AppLocalizations.of(context).favorite_events),
      ),
      body: _data.length >0 ?
      ListView.builder(
          itemCount: _data != null ? _data.length : 0,
          itemBuilder: (BuildContext context, int index) {
            return Container(
              decoration: BoxDecoration(
                border: Border(bottom: BorderSide(width: 0.2,color: Colors.grey))
              ),
              child: ListTile(
                onTap: (){
                  _data[index].isFav = true;
                  Navigator.of(context).push(
                      MaterialPageRoute(
                          builder: (BuildContext context) =>
                              EventDetails(_data[index], _lang))).then((value) {
                                _GetEvent();
                  });
                },
                dense: true,
                contentPadding: EdgeInsets.only(right: 5,left: 5),
                title: Text( _lang == "ar" ?_data[index].eventNameAr :_data[index].eventNameEn,style: OwnColors.normalBlack(),),
                leading:         Padding(
                  padding: const EdgeInsets.all(3.0),
                  child: Container(
                    decoration: BoxDecoration(
                        color: OwnColors.color1[500],
                        borderRadius: BorderRadius.circular(40.0)
                    ),
                    child: Padding(
                      padding: const EdgeInsets.all(5.0),
                      child: ClipOval(

                          child:_data.length > 0
                              ? FutureBuilder<Widget>(
                              future: getImageLogo3(
                                  _data[index].eventTypeID.toString()),
                              builder: (BuildContext context,
                                  AsyncSnapshot<Widget> snapshot) {
                                if (snapshot.hasData)
                                  return snapshot.data;

                                return Icon(EvaIcons.alertTriangleOutline)
                                ;
                              })
                              : Container()
                      ),
                    ),
                  ),
                ),
                trailing: IconButton(icon: Icon(EvaIcons.trash2Outline),color: OwnColors.color2[500], onPressed: (){
                  _delete(_data[index].eventID);

                }),
              ),
            );



          })
          : Center(
        child: DataLoadingUtil().NoData(context),
      ),


    );
  }

  Widget _getImage(String mID) {
    var base = NetworkUtil.internal().imageUrl;
    String rr = base + "EventType/ET$mID.png";
    return Image.network(
      rr,
      width: 50 ,
      height: 50,
      fit: BoxFit.cover,
    );
  }

  _delete(id){
    db.delete(id);
    _GetEvent();
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
