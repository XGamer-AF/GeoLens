import 'package:eva_icons_flutter/eva_icons_flutter.dart';
import 'package:flutter/material.dart';

import '../localizations.dart';
import '../theme.dart';

class More extends StatefulWidget {
  @override
  _MoreState createState() => _MoreState();
}

class _MoreState extends State<More> {
  double _icon_size = 25;
  double _icon_space = 18;
  static const double _padding = 5.0;
  final _font_style = OwnColors.normalBlack();
  final _icon_color = OwnColors.color2[500];
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,

        title: Text(AppLocalizations.of(context).more_title),
      ),
      backgroundColor: OwnColors.color2[50],
      body: Padding(
        padding: const EdgeInsets.all(12.0),
        child: ListView(
          children: [
            Padding(
              padding: const EdgeInsets.only(top: 2, bottom: 2),
              child: _group1(),
            ),
            // Padding(
            //   padding: const EdgeInsets.only(top: 2, bottom: 2),
            //   child: _group1(),
            // ),
          ],
        ),
      ),
    );
  }

// group 1
  Widget _group1() {
    return Container(
      decoration: BoxDecoration(
          color: Colors.white,
          border: Border.all(color: Colors.grey, width: 0.1)),
      child: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          children: [
            InkWell(
              onTap: () {
                Navigator.pushNamed(context, '/fav');
              },
              child: Row(
                children: [
                  Icon(
                    EvaIcons.starOutline,
                    size: _icon_size,
                    color: _icon_color,
                  ),
                  SizedBox(
                    width: _icon_space,
                  ),
                  Text(
                    AppLocalizations.of(context).favorite_events,
                    style: _font_style,
                  )
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(top: _padding, bottom: _padding),
              child: Divider(
                height: 0.1,
                color: OwnColors.color2[50],
              ),
            ),
            InkWell(
              onTap: () {
                Navigator.of(context).pushNamed('/lang');
              },
              child: Row(
                children: [
                  Icon(
                    EvaIcons.globeOutline,
                    size: _icon_size,
                    color: _icon_color,
                  ),
                  SizedBox(
                    width: _icon_space,
                  ),
                  Text(
                    AppLocalizations.of(context).change_lang,
                    style: _font_style,
                  )
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(top: _padding, bottom: _padding),
              child: Divider(
                height: 0.1,
                color: OwnColors.color2[50],
              ),
            ),
            InkWell(
              onTap: () {
                Navigator.of(context).pushNamed('/team');
              },
              child: Row(
                children: [
                  Icon(
                    EvaIcons.peopleOutline,
                    size: _icon_size,
                    color: _icon_color,
                  ),
                  SizedBox(
                    width: _icon_space,
                  ),
                  Text(
                    AppLocalizations.of(context).team_work,
                    style: _font_style,
                  )
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(top: _padding, bottom: _padding),
              child: Divider(
                height: 0.1,
                color: OwnColors.color2[50],
              ),
            ),
            InkWell(
              onTap: () {
                Navigator.pushNamed(context, '/aboutUs');
              },
              child: Row(
                children: [
                  Icon(
                    EvaIcons.infoOutline,
                    size: _icon_size,
                    color: _icon_color,
                  ),
                  SizedBox(
                    width: _icon_space,
                  ),
                  Text(
                    AppLocalizations.of(context).about_us,
                    style: _font_style,
                  )
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(top: _padding, bottom: _padding),
              child: Divider(
                height: 0.1,
                color: OwnColors.color2[50],
              ),
            ),

            InkWell(
              onTap: () {
                Navigator.pushNamed(context, '/slider');
              },
              child: Row(
                children: [
                  Icon(
                    EvaIcons.questionMarkCircleOutline,
                    size: _icon_size,
                    color: _icon_color,
                  ),
                  SizedBox(
                    width: _icon_space,
                  ),
                  Text(
                    AppLocalizations.of(context).help,
                    style: _font_style,
                  )
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
