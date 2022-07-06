import { create } from "@storybook/theming";
import { TyleLogoDarkRedIcon } from "../src/assets/icons/logo";

export default create({
  base: "light",
  brandImage: TyleLogoDarkRedIcon,
  brandTitle: "Tyle",
  brandUrl: "https://github.com/mimir-org/typelibrary",
});
