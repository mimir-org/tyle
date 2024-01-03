import { Theme } from "components/TyleThemeProvider/theme";

export const getButtonColor = (theme?: Theme, buttonColor?: "primary" | "success" | "warning" | "danger" | "error") => {
  switch (buttonColor) {
    case "primary":
      return theme?.color.primary.base;
    case "success":
      return theme?.color.success.base;
    case "warning":
      return theme?.color.tertiary.base;
    case "danger":
    case "error":
      return theme?.color.error.base;
    default:
      return "";
  }
};
