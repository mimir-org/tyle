import { isProduction } from "common/utils/config";
import i18next from "i18next";
import I18nextBrowserLanguageDetector from "i18next-browser-languagedetector";
import en from "locales/en/translation.json";
import { initReactI18next } from "react-i18next";

const resources = {
  en: {
    translation: en,
  },
};

// eslint-disable-next-line import/no-named-as-default-member
i18next.use(initReactI18next).use(I18nextBrowserLanguageDetector).init({
  debug: !isProduction,
  fallbackLng: "en",
  resources,
});
