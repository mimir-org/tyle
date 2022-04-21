import { ObjectType } from "../../../models";

const IsNotSet = (object: ObjectType) => {
  return object === ObjectType.NotSet;
};

export default IsNotSet;
