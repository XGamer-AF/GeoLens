import 'dart:async';
import 'dart:convert';
import '../screens/CookiesClass.dart';
import 'package:http/http.dart' as http;
import 'package:connectivity/connectivity.dart';

class NetworkUtil {
  // next three lines makes this class a Singleton
  static NetworkUtil _instance = new NetworkUtil.internal();
  NetworkUtil.internal();
  factory NetworkUtil() => _instance;

  final JsonDecoder _decoder = new JsonDecoder();
  String imageUrl = "https://taxiq8.com/Images/";
  String baseUrl = "https://taxiq8.com/api/";

//  String baseUrl = "http://localhost:8701/api/";
  Future<dynamic> get(String url) async {
    if (await checkInternetConnection() == false) {
      return 'noConnection';
    }

    return http.get(baseUrl + url).then((http.Response response) {
      final String res = response.body;
      final int statusCode = response.statusCode;

      if (statusCode < 200 || statusCode > 400 || json == null) {
//        throw new Exception("Error while fetching data");
        return false;
      }

      return _decoder.convert(res);
    });
  }

  Future<String> getHeader() async {
    String token;
    var vv = await getValuesSF("vToken");
    if (vv != "") {
      token = vv;
    } else {
      var vv = await getValuesSF("vToken");
      token = vv;
    }
    return token;
  }

  Future<dynamic> post(String url, {Map headers, body, encoding}) {
    return http
        .post(baseUrl + url, body: body, headers: headers, encoding: encoding)
        .then((http.Response response) {
      final String res = response.body;
      final int statusCode = response.statusCode;

      if (statusCode < 200 || statusCode > 400 || json == null) {
        return false;
//        throw new Exception("Error while fetching data");
      }
      return _decoder.convert(res);
    });
  }

  Future<dynamic> put(String url, {Map headers, body, encoding}) {
    return http
        .put(baseUrl + url, body: body, headers: headers, encoding: encoding)
        .then((http.Response response) {
      final String res = response.body;
      final int statusCode = response.statusCode;

      if (statusCode < 200 || statusCode > 400 || json == null) {
        return false;
//        throw new Exception("Error while fetching data");
      }

      return _decoder.convert(res);
    });
  }

  Future<String> getString(String url) {
    return http.get(baseUrl + url).then((http.Response response) {
      final String res = response.body;
      final int statusCode = response.statusCode;

      if (statusCode < 200 || statusCode > 400 || json == null) {
//        throw new Exception("Error while fetching data");
        return "false";
      }

      return (res);
    });
  }

  Future<bool> checkInternetConnection() async {
    var connectivityResult = await (Connectivity().checkConnectivity());
    if (connectivityResult == ConnectivityResult.mobile) {
      return true;
    } else if (connectivityResult == ConnectivityResult.wifi) {
      return true;
    }
    return false;
  }
}
