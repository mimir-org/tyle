import { Moon, Sun } from "@styled-icons/heroicons-outline";
import { usePrefersTheme } from "@mimirorg/component-library";
import { toggleDarkTheme } from "components/Header/ThemeButton.helpers";
import { UserMenuButton } from "components/Header/UserMenuButton";
import { useTranslation } from "react-i18next";

export const ThemeButton = () => {
  const { t } = useTranslation("ui");
  const [colorTheme] = usePrefersTheme("tyleLight");
  const isDarkModeEnabled = colorTheme === "tyleDark";

  return (
    <UserMenuButton icon={isDarkModeEnabled ? <Sun size={24} /> : <Moon size={24} />} onClick={() => toggleDarkTheme()}>
      {isDarkModeEnabled ? t("header.menu.lightMode") : t("header.menu.darkMode")}
    </UserMenuButton>
  );
};
