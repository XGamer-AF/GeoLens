import 'dart:async';
import 'package:data_discovery_for_earth/Models/event_info_model.dart';
import 'package:data_discovery_for_earth/Services/network_util.dart';
import 'package:eva_icons_flutter/eva_icons_flutter.dart';
import 'package:flutter/material.dart';
import 'package:dio/dio.dart';
import '../Models/place.dart';
import 'package:auto_size_text/auto_size_text.dart';
import 'package:google_maps_webservice/places.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import '../Screens/CookiesClass.dart';
import 'package:geolocator/geolocator.dart';

import '../theme.dart';
import 'event_details.dart';
import 'home.dart';

const String apiKey = " AIzaSyBa67HtFOhUvL1-A1lq9x0957yW8vj1c2k";

GoogleMapsPlaces _places = GoogleMapsPlaces(apiKey: apiKey);

class EventsMap extends StatefulWidget {
  @override
  _EventsMapState createState() => _EventsMapState();
}

class _EventsMapState extends State<EventsMap> {
  String _lang = "";
  BuildContext _myContext;
  bool _isVisable = false;
  double _zoom = 4.0;
  LatLng latLngCamera = new LatLng(29.13522200, 48.13138900);
  TextEditingController _textcontroller = TextEditingController();
  Timer _timer;
  Completer<GoogleMapController> _controller = Completer();
  double lat = 29.13522200;
  double long = 48.13138900;
  List<EventInfoModel> _data = [];
  Set<Marker> markers = Set();
  void getLocation() async {
    Position position = await Geolocator()
        .getCurrentPosition(desiredAccuracy: LocationAccuracy.high);
    setState(() {
      lat = position.latitude;
      long = position.longitude;
    });
    final GoogleMapController controller = await _controller.future;

    controller.animateCamera(CameraUpdate.newCameraPosition(CameraPosition(
        target: LatLng(position.latitude, position.longitude), zoom: 4)));
  }

  void getLocation14() async {
    Position position = await Geolocator()
        .getCurrentPosition(desiredAccuracy: LocationAccuracy.high);
    setState(() {
      lat = position.latitude;
      long = position.longitude;
    });
    final GoogleMapController controller = await _controller.future;

    controller.animateCamera(CameraUpdate.newCameraPosition(CameraPosition(
        target: LatLng(position.latitude, position.longitude), zoom: 14)));
  }

  void _onMapCreated(GoogleMapController controller) {
    getLocation();
    _controller.complete(controller);
  }

  _onSearchChange() {
    if (_timer?.isActive ?? false) _timer.cancel();
    _timer = Timer(const Duration(milliseconds: 300), () {
      getLocationResult(_textcontroller.text);
    });
  }

  _GetEvent() async {
    List<EventInfoModel> result = [];
    final query = "AEventInfo";
    var resultJson = await NetworkUtil.internal().get(query);
    if (resultJson == false || resultJson == 'noConnection') {
      return resultJson;
    } else {
      for (var item in resultJson) {
        result.add(EventInfoModel.fromJson(item));
      }
      setState(() {
        _data = result;
      });
      for (var item in result) {
        setState(() {
          markers.add(Marker(
            markerId: MarkerId(item.eventID.toString()),
            position: LatLng(double.parse(item.latitude.toString()),
                double.parse(item.longitude.toString())),
            infoWindow: InfoWindow(
                title: _lang == "ar"
                    ? item.eventNameAr.toString()
                    : item.eventNameEn.toString(),
                onTap: () {
                  Navigator.of(context).push(MaterialPageRoute(
                      builder: (BuildContext context) =>
                          EventDetails(item, _lang)));
                }),
          ));
        });
      }
    }
  }

  List<Place> _placesAll = [];
  Widget buildPlaceCard(BuildContext context, f) {
    return Container(
      color: Colors.white,
      child: InkWell(
        onTap: () {
          displayPrediction(f.id, f.name);
          setState(() {
//            _textcontroller.text = f.name;
            _placesAll.clear();
          });
        },
        child: Padding(
          padding: const EdgeInsets.only(
              right: 5.0, left: 5.0, top: 3.0, bottom: 3.0),
          child: Row(
            children: <Widget>[
              Icon(Icons.location_on),
              Flexible(
                child: AutoSizeText(
                  f.name,
                  maxLines: 4,
                  style: Theme.of(context).textTheme.body2,
                ),
              )
            ],
          ),
        ),
      ),
    );
  }

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    getLang();
    _GetEvent();
    _textcontroller.addListener(_onSearchChange);
  }

  @override
  void dispose() {
    // TODO: implement dispose
    _textcontroller.removeListener(_onSearchChange);
    _textcontroller.dispose();

    super.dispose();
  }

  Future<String> getLang() async {
    return await getValuesSF("lang");
  }

  @override
  Widget build(BuildContext context) {
    _myContext = context;
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        leading: IconButton(
          icon: Icon(EvaIcons.navigationOutline),
          onPressed: () {
            getLocation14();
          },
        ),

        centerTitle: true,
        backgroundColor: OwnColors.color2[500],
        actions: <Widget>[
          Padding(
            padding: const EdgeInsets.only(right: 0, left: 0),
            child: Container(
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: <Widget>[
                  Padding(
                    padding: const EdgeInsets.only(right: 10.0, left: 10.0),
                    child: Text(
                        double.parse(lat.toStringAsFixed(8)).toString() +
                            "," +
                            double.parse(long.toStringAsFixed(8)).toString()),
                  ),
                  Padding(
                    padding: const EdgeInsets.only(right: 10.0, left: 10.0),
                    child: Icon(EvaIcons.pinOutline),
                  )
                ],
              ),
            ),
          ),
        ],
//        title: Text(AppLocalizations.of(context).btnAdd,
//            style: Theme.of(context).textTheme.headline),
      ),
      body: Padding(
          padding: EdgeInsets.all(5),
          child: Stack(
            children: <Widget>[
              GoogleMap(
                onTap: (latlgn) {
                  _onTap(latlgn);
                },
                onLongPress: (latlgn) {},
                onMapCreated: _onMapCreated,
                myLocationEnabled: true,
                zoomGesturesEnabled: true,
                scrollGesturesEnabled: true,
                rotateGesturesEnabled: true,
                tiltGesturesEnabled: true,
                mapType: MapType.normal,
                initialCameraPosition: CameraPosition(
                  bearing: 270.0,
                  tilt: 30.0,
                  target: latLngCamera,
                  zoom: _zoom,
                ),
                markers: markers,
              ),
              Column(
                children: <Widget>[
                  Container(
                    decoration: BoxDecoration(
                        color: Colors.white,
                        border: Border(
                            bottom:
                                BorderSide(width: 0.2, color: Colors.black))),
                    child: TextField(
                      style: OwnColors.normalBlack(),
                      controller: _textcontroller,
                      decoration: InputDecoration(
                          prefixIcon: Icon(Icons.search),
                          suffixIcon: InkWell(
                            child: Icon(Icons.clear),
                            onTap: () {
                              setState(() {
                                _textcontroller.clear();
                                _placesAll.clear();
                              });
                            },
                          )),
                    ),
                  ),

                  Column(
                    children: _placesAll.map((f) {
                      return buildPlaceCard(context, f);
                    }).toList(),
                  )
//                  Expanded(
//                      child: _placesAll != null
//                          ? ListView.builder(
//                              itemCount: _placesAll.length,
//                              itemBuilder: (BuildContext context, int index) =>
//                                  buildPlaceCard(context, index),
//                            )
//                          : Container())
                ],
              ),
            ],
          )),
    );
  }
//  Map<MarkerId, Marker> markers = <MarkerId, Marker>{}; // CLASS MEMBER, MAP OF MARKS

//  Set<Marker> markers = Set();

  void getLocationResult(String input) async {
    if (input.isEmpty) {
      return;
    }
    String baseUrl =
        "https://maps.googleapis.com/maps/api/place/autocomplete/json";
    String type = "(region)";
    String request =
        "$baseUrl?input=$input&key=$apiKey&language=ar&type=geocode";
    Response response = await Dio().get(request);
    final predictions = response.data["predictions"];
    List<Place> _places = [];
    for (var i = 0; i < predictions.length; i++) {
      String name = predictions[i]["description"];
      double aver = 200.0;
      String id = predictions[i]["place_id"];
      _places.add(Place(name, aver.toString(), id));
    }
    setState(() {
      _placesAll = _places;
    });
  }

  Future<Null> displayPrediction(String p, String name) async {
    if (p != null) {
      PlacesDetailsResponse detail = await _places.getDetailsByPlaceId(p);

      var placeId = p;
      double lat = detail.result.geometry.location.lat;
      double lng = detail.result.geometry.location.lng;

      setState(() {
        lat = detail.result.geometry.location.lat;
        ;
        long = detail.result.geometry.location.lng;
        ;
      });
      final location = Location(lat, lng);
//      Map<MarkerId, Marker> markers1 = <MarkerId, Marker>{}; // CLASS MEMBER, MAP OF MARKS
      final mm = Marker(
        onTap: () {
          setState(() {
            lat = location.lat;
            long = location.lng;
          });
        },

        markerId: MarkerId(p),
        position: LatLng(lat, lng),
        infoWindow:
            InfoWindow(title: detail.result.name, snippet: detail.result.name),
        visible: true,
        draggable: true,
//        icon: BitmapDescriptor.defaultMarker,
      );
      final GoogleMapController controller = await _controller.future;
      setState(() {
//        markers.clear();
//        markers[MarkerId(p)] = mm;
//        markers = markers1;
        latLngCamera = LatLng(lat, lng);
        controller.animateCamera(CameraUpdate.newCameraPosition(
            CameraPosition(target: LatLng(lat, lng), zoom: 8)));
      });
    }
  }

  void _onTap(LatLng latlgn) {
    final mm = Marker(
      onTap: () {
        setState(() {
          lat = latlgn.latitude;
          long = latlgn.longitude;
        });
      },

      markerId: MarkerId("1"),
      position: LatLng(latlgn.latitude, latlgn.longitude),
      infoWindow: InfoWindow(title: "Location", snippet: "Location"),
      visible: true,
      draggable: true,
//        icon: BitmapDescriptor.defaultMarker,
    );

    setState(() {
//      markers.clear();
      lat = latlgn.latitude;
      long = latlgn.longitude;
//      markers[MarkerId("1")] = mm;
    });
  }

  void _onMarkerTapped(Location markerId) {
    print("markerId");
  }
}
