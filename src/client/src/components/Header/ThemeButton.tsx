import { usePrefersTheme } from "@mimirorg/component-library";
import { Moon, Sun } from "@styled-icons/heroicons-outline";
import { useTranslation } from "react-i18next";
import { toggleDarkTheme } from "./ThemeButton.helpers";
import UserMenuButton from "./UserMenuButton";

const ThemeButton = () => {
  const { t } = useTranslation("ui");
  const [colorTheme] = usePrefersTheme("tyleLight");
  const isDarkModeEnabled = colorTheme === "tyleDark";

  return (
    <UserMenuButton icon={isDarkModeEnabled ? <Sun size={24} /> : <Moon size={24} />} onClick={() => toggleDarkTheme()}>
      {isDarkModeEnabled ? t("header.menu.lightMode") : t("header.menu.darkMode")}
    </UserMenuButton>
  );
};

export default ThemeButton;
