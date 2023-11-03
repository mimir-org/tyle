import i18next from "i18next";
import I18nextBrowserLanguageDetector from "i18next-browser-languagedetector";
import enAuth from "locales/en/auth.json";
import enCommon from "locales/en/common.json";
import enEntities from "locales/en/entities.json";
import enExplore from "locales/en/explore.json";
import enSettings from "locales/en/settings.json";
import enUi from "locales/en/ui.json";
import { initReactI18next } from "react-i18next";
import { isProduction } from "./config";

const resources = {
  en: {
    auth: enAuth,
    common: enCommon,
    entities: enEntities,
    explore: enExplore,
    settings: enSettings,
    ui: enUi,
  },
};

// eslint-disable-next-line import/no-named-as-default-member
i18next.use(initReactI18next).use(I18nextBrowserLanguageDetector).init({
  debug: !isProduction,
  fallbackLng: "en",
  resources,
});
