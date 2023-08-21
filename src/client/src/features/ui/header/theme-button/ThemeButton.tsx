import { Moon, Sun } from "@styled-icons/heroicons-outline";
import { usePrefersTheme } from "@mimirorg/component-library";
import { toggleDarkTheme } from "features/ui/header/theme-button/ThemeButton.helpers";
import { UserMenuButton } from "features/ui/header/user-menu/UserMenuButton";
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
