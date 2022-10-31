import { ColorReference } from "complib/core/variables/color/types/colorReference";
import { ColorTheme } from "complib/core/variables/color/types/colorTheme";

export interface ColorSystem {
  ref: ColorReference,
  sys: ColorTheme
}