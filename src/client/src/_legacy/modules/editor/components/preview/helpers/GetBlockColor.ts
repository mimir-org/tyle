import { Color } from "../../../../../../compLibrary/colors";
import { Aspect } from "../../../../../models";
import { IsFunction, IsLocation, IsProduct } from "../../../helpers";

const GetBlockColor = (aspect: Aspect) => {
  let color = "";
  if (IsFunction(aspect)) {
    color = Color.FunctionMain;
  } else if (IsLocation(aspect)) {
    color = Color.LocationMain;
  } else if (IsProduct(aspect)) {
    color = Color.ProductMain;
  }
  return color;
};

export default GetBlockColor;
