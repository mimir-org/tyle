import { Moon, Sun } from "@styled-icons/heroicons-outline";
import { usePrefersTheme } from "common/hooks/usePrefersTheme";
import { toggleDarkTheme } from "features/ui/header/theme-button/ThemeButton.helpers";
import { UserMenuButton } from "features/ui/header/user-menu/UserMenuButton";
import { useTranslation } from "react-i18next";

export const ThemeButton = () => {
  const { t } = useTranslation("ui");
  const [colorTheme] = usePrefersTheme("light");
  const isDarkModeEnabled = colorTheme == "dark";

  return (
    <UserMenuButton icon={isDarkModeEnabled ? <Sun size={24} /> : <Moon size={24} />} onClick={() => toggleDarkTheme()}>
      {isDarkModeEnabled ? t("header.menu.lightMode") : t("header.menu.darkMode")}
    </UserMenuButton>
  );
};
