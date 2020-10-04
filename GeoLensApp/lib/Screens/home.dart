import 'package:chewie/chewie.dart';
import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:video_player/video_player.dart';

import '../localizations.dart';
import '../theme.dart';

class Home extends StatefulWidget {
  @override
  _HomeState createState() => _HomeState();
}

class _HomeState extends State<Home> {
  VideoPlayerController videoPlayerController;
  bool looping;
  ChewieController _chewieController = new ChewieController(
    videoPlayerController:
        VideoPlayerController.network('https://taxiq8.com/Images/intro.mp4'),
    aspectRatio: 16 / 9,
    autoInitialize: true,
    autoPlay: false,
    looping: false,
    errorBuilder: (context, errorMessage) {
      return Center(
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Text(
            errorMessage,
            style: TextStyle(color: Colors.white),
          ),
        ),
      );
    },
  );

  @override
  void dispose() {
    super.dispose();
    // IMPORTANT to dispose of all the used resources
    videoPlayerController.dispose();
    _chewieController.dispose();
  }

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    @override
    void initState() {
      super.initState();
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        bottomNavigationBar: InkWell(
          onTap: () {
            _launchURL(
                'https://www.youtube.com/playlist?list=PL33cJND8MkkIIUPh6Lt3Yan2SeImfceyb');
          },
          child: Padding(
            padding: const EdgeInsets.all(0),
            child: Container(
              decoration: BoxDecoration(
                  color: OwnColors.color2[500],
                  border: Border(
                      bottom: BorderSide(width: 0.1, color: Colors.white))),
              width: MediaQuery.of(context).size.width,
              child: Padding(
                padding: const EdgeInsets.all(15.0),
                child: Text(
                  AppLocalizations.of(context).link,
                  style: TextStyle(color: OwnColors.color1[500]),
                  textAlign: TextAlign.center,
                ),
              ),
            ),
          ),
        ),
        appBar: AppBar(
          centerTitle: true,
          leading: Container(),
          title: Text(
            AppLocalizations.of(context).home_title,
          ),
        ),
        body: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                AppLocalizations.of(context).guide,
                style: TextStyle(
                    color: OwnColors.color2[500],
                    fontWeight: FontWeight.w500,
                    fontSize: 17),
              ),
              SizedBox(
                height: 10,
              ),
              Chewie(
                controller: _chewieController,
              ),
            ],
          ),
        ));
  }

  _launchURL(String url) async {
    if (await canLaunch(url)) {
      await launch(url);
    } else {
      throw 'Could not launch $url';
    }
  }
}
