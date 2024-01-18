import { Moon, Sun } from "@styled-icons/heroicons-outline";
import { usePrefersTheme } from "hooks/usePrefersTheme";
import { toggleDarkTheme } from "./ThemeButton.helpers";
import UserMenuButton from "./UserMenuButton";

const ThemeButton = () => {
  const [colorTheme] = usePrefersTheme("light");
  const isDarkModeEnabled = colorTheme === "dark";

  return (
    <UserMenuButton icon={isDarkModeEnabled ? <Sun size={24} /> : <Moon size={24} />} onClick={() => toggleDarkTheme()}>
      {isDarkModeEnabled ? "Enable light mode" : "Enable dark mode"}
    </UserMenuButton>
  );
};

export default ThemeButton;
