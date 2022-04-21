import { Color } from "../../compLibrary/colors";
import { AspectColorType, LibItem, Node } from "../models";
import { IsFunction, IsLocation, IsProduct } from "./index";

/**
 * Component to get the color for a given item.
 * @param node
 * @param colorType
 * @param isTransparent
 * @returns the color according to the chosen criteriums.
 */
const GetAspectColor = (node: Node | LibItem, colorType: AspectColorType, isTransparent?: boolean) => {
  if (isTransparent) {
    if (IsFunction(node)) return "rgba(251, 201, 19, 0.1)";
    if (IsProduct(node)) return "rgba(6, 144, 152, 0.1)";
    if (IsLocation(node)) return "rgba(163, 0, 167, 0.1)";
  }

  if (colorType === AspectColorType.Main) return GetMainColor(node);
  if (colorType === AspectColorType.Selected) return GetSelectedColor(node);
  if (colorType === AspectColorType.Header) return GetHeaderColor(node);
  if (colorType === AspectColorType.Tab) return GetTabColor(node);
};

function GetMainColor(node: Node | LibItem) {
  if (IsFunction(node)) return Color.FunctionMain;
  if (IsProduct(node)) return Color.ProductMain;
  if (IsLocation(node)) return Color.LocationMain;
}

function GetSelectedColor(node: Node | LibItem) {
  if (IsFunction(node)) return Color.FunctionSelected;
  if (IsProduct(node)) return Color.ProductSelected;
  if (IsLocation(node)) return Color.LocationSelected;
}

function GetHeaderColor(node: Node | LibItem) {
  if (IsFunction(node)) return Color.FunctionHeader;
  if (IsProduct(node)) return Color.ProductHeader;
  if (IsLocation(node)) return Color.LocationHeader;
}

function GetTabColor(node: Node | LibItem) {
  if (IsFunction(node)) return Color.FunctionTab;
  if (IsProduct(node)) return Color.ProductTab;
  if (IsLocation(node)) return Color.LocationTab;
}

export default GetAspectColor;
