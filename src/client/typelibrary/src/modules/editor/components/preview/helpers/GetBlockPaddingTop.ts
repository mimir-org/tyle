import { Aspect } from "../../../../../models";
import { IsFunction, IsLocation } from "../../../helpers";

const GetBlockPaddingTop = (aspect: Aspect) => {
  let top = 0;
  if (IsFunction(aspect)) {
    top = 5;
  } else if (IsLocation(aspect)) {
    top = 1;
  }
  return top;
};

export default GetBlockPaddingTop;
