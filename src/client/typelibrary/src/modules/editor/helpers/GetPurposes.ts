import { DropDownCategoryItem } from "../components/dropdown/Dropdown";
import { Purpose } from "../../../models";
import { CreateId } from "../../../helpers";

const GetPurposes = (purposes: Purpose[]): DropDownCategoryItem<Purpose>[] => {
  const categories = [] as DropDownCategoryItem<Purpose>[];
  categories.push({ id: CreateId(), name: "Purposes", description: "Purpose", image: "", items: [] });

  purposes.forEach((purpose) => {
    categories[0].items.push({
      id: purpose.id,
      name: purpose.name,
      description: purpose.name,
      image: "",
      value: purpose,
    });
  });
  return categories;
};

export default GetPurposes;
