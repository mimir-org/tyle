import { create } from "@storybook/theming";
import { LibraryIcon } from "../src/assets/icons/modules";

export default create({
  base: "light",
  brandImage: LibraryIcon,
  brandTitle: "Tyle",
  brandUrl: "https://github.com/mimir-org/typelibrary",
});
