import { isProduction } from "common/utils/config";
import i18next from "i18next";
import I18nextBrowserLanguageDetector from "i18next-browser-languagedetector";
import enExplore from "locales/en/explore.json";
import enSettings from "locales/en/settings.json";
import enRoot from "locales/en/translation.json";
import { initReactI18next } from "react-i18next";

const resources = {
  en: {
    translation: enRoot,
    explore: enExplore,
    settings: enSettings,
  },
};

// eslint-disable-next-line import/no-named-as-default-member
i18next.use(initReactI18next).use(I18nextBrowserLanguageDetector).init({
  debug: !isProduction,
  fallbackLng: "en",
  resources,
});
