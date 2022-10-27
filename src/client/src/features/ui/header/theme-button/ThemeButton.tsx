import { Moon, Sun } from "@styled-icons/heroicons-outline";
import { usePrefersTheme } from "common/hooks/usePrefersTheme";
import { useTranslation } from "react-i18next";
import { UserMenuButton } from "../user-menu/UserMenuButton";
import { toggleDarkTheme } from "./ThemeButton.helpers";

export const ThemeButton = () => {
  const { t } = useTranslation();
  const [colorTheme] = usePrefersTheme("light");
  const isDarkModeEnabled = colorTheme == "dark";

  return (
    <UserMenuButton icon={isDarkModeEnabled ? <Sun size={24} /> : <Moon size={24} />} onClick={() => toggleDarkTheme()}>
      {isDarkModeEnabled ? t("user.menu.lightMode") : t("user.menu.darkMode")}
    </UserMenuButton>
  );
};
