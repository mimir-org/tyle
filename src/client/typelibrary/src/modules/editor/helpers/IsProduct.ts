import { Aspect } from "../../../models";

const IsProduct = (aspect: Aspect) => {
  return aspect === Aspect.Product;
};

export default IsProduct;
