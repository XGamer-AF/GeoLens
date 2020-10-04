import 'package:eva_icons_flutter/eva_icons_flutter.dart';

import '../localizations.dart';
import 'package:flutter/material.dart';

import '../theme.dart';

class DataLoadingUtil {
  final double _icon_size = 100;

  Widget LoadingData() {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Image.asset('assets/Images/logo.gif', width: 50, height: 50),
        ],
      ),
    );
  }

  Widget NoData(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Image.asset(
            'assets/Images/noData.png',
            height: 150,
            width: 150,
          ),
          Text(AppLocalizations.of(context).error_no_data),
        ],
      ),
    );
  }

  Widget NoConnection(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Padding(
            padding: const EdgeInsets.all(20.0),
            child: Icon(
              Icons.settings,
              size: _icon_size,
            ),
          ),
          Text(
            AppLocalizations.of(context).errorConnection,
            style: OwnColors.normalBlack(),
          )
        ],
      ),
    );
  }
}
