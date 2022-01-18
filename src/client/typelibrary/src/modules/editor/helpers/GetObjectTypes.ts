import { GetObjectTypeDescription } from "./index";
import { DropDownCategoryItem } from "../components/dropdown/Dropdown";
import { Aspect, ObjectType } from "../../../models";
import { ObjectTypeKey } from "../types";
import { CreateId } from "../../../helpers";

const stringIsNumber = (value: unknown) => !isNaN(Number(value));

const GetObjectTypes = (aspect: Aspect): DropDownCategoryItem<ObjectTypeKey>[] => {
  const categories = [] as DropDownCategoryItem<ObjectTypeKey>[];
  categories.push({ id: CreateId(), name: "ObjectType", description: "Object Type", image: null, items: [] });

  Object.keys(ObjectType)
    .filter(stringIsNumber)
    .forEach((item) => {
      categories[0].items.push({
        id: item,
        name: ObjectType[item],
        description: GetObjectTypeDescription(aspect, Number(item)),
        image: null,
        value: ObjectType[item] as ObjectTypeKey,
      });
    });

  return categories;
};

export default GetObjectTypes;
