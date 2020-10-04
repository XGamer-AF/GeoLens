import 'dart:async';
import 'dart:convert';
import 'dart:ffi';
import 'package:data_discovery_for_earth/Models/event_info_model.dart';
import 'package:data_discovery_for_earth/Models/event_types_model.dart';
import 'package:sqflite/sqflite.dart';
import 'dart:io';
import 'package:path_provider/path_provider.dart';
import 'package:path/path.dart';
import '../Models/event_info_model.dart';

import 'network_util.dart';

class DatabaseHelper {
  static final DatabaseHelper _instance = new DatabaseHelper.internal();
  factory DatabaseHelper() => _instance;
  DatabaseHelper.internal();
  final eventInfo = "eventInfo";

  String eventID = "eventID";
  String eventNameEn = "eventNameEn";
  String eventNameAr = "eventNameAr";
  String eventDescEn = "eventDescEn";
  String eventDescAr = "eventDescAr";
  String latitude = "latitude";
  String longitude = "longitude";
  String eventTypeID = "eventTypeID";
  String countryID = "countryID";
  String eventDate = "eventDate";
  String userID ="userID";
  String statusID ="statusID";
  String eventImage ="eventImage";

  static Database _db;
  Future<Database> get db async {
    if (_db != null) {
      return _db;
    }
    _db = await initDB();
    return _db;
  }



  initDB() async {
    Directory docDir = await getApplicationDocumentsDirectory();
    String path = join(docDir.path, "maindb2.db");
    var ourDb = await openDatabase(path, version: 1, onCreate: _onCreate);
    return ourDb;
  }

  void _onCreate(Database db, int version) async {
    db.execute(
        "CREATE TABLE $eventInfo ($eventID INTEGER, $eventNameEn TEXT , $eventNameAr TEXT , $eventDescEn TEXT ,$eventDescAr TEXT, $latitude DOUBLE ,$longitude DOUBLE,$eventTypeID INTEGER , $countryID INTEGER  ,$eventDate TEXT,$userID INTEGER,$statusID INTEGER,$eventImage TEXT)");
  }

  Future<int> savePost(EventInfoModel row) async {
    var dbClient = await db;
//    row.logoImage =await getImageLogo(row.memberID.toString());

    var res =await dbClient.insert("$eventInfo", row.toJson());
    return res;
  }



  Future<List<EventInfoModel>> getAll() async {
    List<EventInfoModel> obj=[];
    var dbClient = await db;
    var result = await dbClient.rawQuery("SELECT * FROM $eventInfo");
    for (var item in result) {
      obj.add(EventInfoModel.fromJson(item));
    }
    return obj;
  }

  Future<EventInfoModel> get(int id) async {
    var dbClient = await db;
    var result = await dbClient
        .rawQuery("SELECT * FROM $eventInfo WHERE $eventID = $id");
    if (result.length == 0) return null;

    return new EventInfoModel.fromJson(result.first);
  }
  Future<int> delete (int id) async{
    var dbClient =await db;
    return await dbClient.delete(eventInfo,where: "$eventID = ?",whereArgs: [id]);


  }
  Future<int> update (EventInfoModel row) async{
    var dbClient =await db;
    return await dbClient.update(eventInfo, row.toJson(),where: "$eventID =?",whereArgs: [row.eventID]);
  }

  Future<bool> deleteAll() async
  {
    List result = await getAll();
      if(result.length >0)
        {
          result.forEach((f){
            delete(f["$eventID"]);
          });
        }
        return true;
  }

    Future close()async{
      var dbClient =await db;
      return dbClient.close();
    }







}
