import { create } from "@storybook/theming";
import { TyleLogoDarkRedIcon } from "../src/common/components/logo/assets";

export default create({
  base: "light",

  colorPrimary: "#3d113f",
  colorSecondary: "#39862d",

  appBg: "#f7f6ff",
  appContentBg: "#ffffff",
  appBorderColor: "#cbcbcb",
  appBorderRadius: "10px",

  fontBase: '"roboto", sans-serif',

  textColor: "#13131c",
  textInverseColor: "#fdfbff",

  barTextColor: "#6f6f6f",
  barBg: "#fcfcfc",

  inputBg: "#ffffff",
  inputBorder: "#cbcbcb",
  inputTextColor: "#13131c",
  inputBorderRadius: "5px",

  brandImage: TyleLogoDarkRedIcon,
  brandTitle: "Tyle",
  brandUrl: "https://github.com/mimir-org/typelibrary",
});
