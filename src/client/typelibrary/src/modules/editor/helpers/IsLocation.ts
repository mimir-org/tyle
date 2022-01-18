import { Aspect } from "../../../models";

const IsLocation = (aspect: Aspect) => {
  return aspect === Aspect.Location;
};

export default IsLocation;
