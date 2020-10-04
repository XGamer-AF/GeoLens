import 'dart:async';
import 'dart:ui';
import 'package:flutter/cupertino.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'l10n/messages_all.dart';

class AppLocalizations {
  static AppLocalizations of(BuildContext context) {
    return Localizations.of<AppLocalizations>(context, AppLocalizations);
  }

  static Future<AppLocalizations> load(Locale locale) {
    final String name =
        locale.countryCode == null ? locale.languageCode : locale.toString();
    final String localeName = Intl.canonicalizedLocale(name);

    return initializeMessages(localeName).then((bool _) {
      Intl.defaultLocale = localeName;
      return new AppLocalizations();
    });
  }

// Pages
  String get home_title {
    return Intl.message('Home', name: 'home_title');
  }

  String get categories_title {
    return Intl.message('Categories', name: 'categories_title');
  }

  String get events_title {
    return Intl.message('ُEvents', name: 'events_title');
  }

  String get remainder_title {
    return Intl.message('Remainder', name: 'remainder_title');
  }

  String get map_title {
    return Intl.message('Map', name: 'map_title');
  }

  String get more_title {
    return Intl.message('More', name: 'more_title');
  }

  String get about_title {
    return Intl.message('AboutUS', name: 'about_title');
  }

  String get lang_title {
    return Intl.message('Change Language', name: 'lang_title');
  }

  // *MORE PAGE* ----------------------------------------------

  String get favorite_events {
    return Intl.message('Favorite Events', name: 'favorite_events');
  }

  String get about_us {
    return Intl.message('About Us', name: 'about_us');
  }

  String get team_work {
    return Intl.message('Team Work', name: 'team_work');
  }

  String get change_lang {
    return Intl.message('Change Language', name: 'change_lang');
  }

  String get help {
    return Intl.message('Help', name: 'help');
  }

  String get change {
    return Intl.message('Change', name: 'change');
  }

  // *Events PAGE* ----------------------------------------------

  String get search_label {
    return Intl.message('Search', name: 'search_label');
  }

  String get more_label {
    return Intl.message('Read More', name: 'more_label');
  }

  String get refrences_label {
    return Intl.message('Sources & Refrences', name: 'refrences_label');
  }

  String get allEvents {
    return Intl.message('All Events', name: 'allEvents');
  }

  String get latestEvents {
    return Intl.message('Latest Events', name: 'latestEvents');
  }
  // OnBoarding Screen

  String get getStarted_label {
    return Intl.message('GET STARTED NOW', name: 'getStarted_label');
  }

  String get next_label {
    return Intl.message('NEXT', name: 'next_label');
  }

  String get skip_label {
    return Intl.message('SKIP', name: 'skip_label');
  }

  // Language & Countries Page
  String get btn_change {
    return Intl.message('Change', name: 'btn_change');
  }

  // Home
  String get guide {
    return Intl.message('Application Guide', name: 'guide');
  }

  String get link {
    return Intl.message('To Watch Video On Youtube', name: 'link');
  }

  // error Messages
  String get errorConnection {
    return Intl.message('Internet connection error', name: 'errorConnection');
  }

  String get error_no_data {
    return Intl.message('There is no data', name: 'error_no_data');
  }
}

class SpecificLocalizationDelegate
    extends LocalizationsDelegate<AppLocalizations> {
  final Locale overriddenLocale;

  SpecificLocalizationDelegate(this.overriddenLocale);

  @override
  bool isSupported(Locale locale) => overriddenLocale != null;

  @override
  Future<AppLocalizations> load(Locale locale) =>
      AppLocalizations.load(overriddenLocale);

  @override
  bool shouldReload(LocalizationsDelegate<AppLocalizations> old) => true;
}

class FallbackCupertinoLocalisationsDelegate
    extends LocalizationsDelegate<CupertinoLocalizations> {
  const FallbackCupertinoLocalisationsDelegate();

  @override
  bool isSupported(Locale locale) => ['ar', 'en'].contains(locale.languageCode);

  @override
  Future<CupertinoLocalizations> load(Locale locale) =>
      SynchronousFuture<_DefaultCupertinoLocalizations>(
          _DefaultCupertinoLocalizations(locale));

  @override
  bool shouldReload(FallbackCupertinoLocalisationsDelegate old) => false;
}

class _DefaultCupertinoLocalizations extends DefaultCupertinoLocalizations {
  final Locale locale;

  _DefaultCupertinoLocalizations(this.locale);
}
