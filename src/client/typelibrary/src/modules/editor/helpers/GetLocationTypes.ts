import { DropDownCategoryItem } from "../components/dropdown/Dropdown";
import { LocationType } from "../../../models";

const GetLocationTypes = (locations: LocationType[]): DropDownCategoryItem<LocationType>[] => {
  const categories = [] as DropDownCategoryItem<LocationType>[];
  if (!locations || locations.length <= 0) return categories;

  locations.forEach((category) => {
    const cat: DropDownCategoryItem<LocationType> = { id: category.id, name: category.name, description: category.name, image: "", items: [] };
    category?.locationSubTypes.forEach((element) => {
      cat.items.push({
        id: element.id,
        name: element.name,
        description: element.name,
        image: "",
        value: element,
      });
    });
    categories.push(cat);
  });

  return categories;
};

export default GetLocationTypes;
