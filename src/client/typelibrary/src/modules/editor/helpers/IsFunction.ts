import { Aspect } from "../../../models";

const IsFunction = (aspect: Aspect) => {
  return aspect === Aspect.Function;
};

export default IsFunction;
