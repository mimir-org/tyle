import { usePrefersTheme } from "@mimirorg/component-library";
import { Moon, Sun } from "@styled-icons/heroicons-outline";
import { toggleDarkTheme } from "./ThemeButton.helpers";
import UserMenuButton from "./UserMenuButton";

const ThemeButton = () => {
  const [colorTheme] = usePrefersTheme("tyleLight");
  const isDarkModeEnabled = colorTheme === "tyleDark";

  return (
    <UserMenuButton icon={isDarkModeEnabled ? <Sun size={24} /> : <Moon size={24} />} onClick={() => toggleDarkTheme()}>
      {isDarkModeEnabled ? "Enable light mode" : "Enable dark mode"}
    </UserMenuButton>
  );
};

export default ThemeButton;
