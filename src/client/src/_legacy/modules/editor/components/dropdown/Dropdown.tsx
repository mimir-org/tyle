import { useCallback, useEffect, useState } from "react";
import { CollapseIcon, ExpandIcon } from "../../../../../assets/icons/chevron";
import { LocationTypeCategory } from "../../styled";
import { Symbol } from "../../../../../compLibrary/symbol";
import { DropdownMenuHeader, DropdownMenuList, DropdownMenuListItem, DropdownMenuWrapper } from "./styled";

export interface DropDownCategoryItem<T> {
  id: string;
  name: string;
  description: string;
  items: DropDownItem<T>[];
  image: string;
}

export interface DropDownItem<T> {
  id: string;
  name: string;
  image: string;
  description: string;
  value: T;
}

interface Props<T> {
  label: string;
  categories: DropDownCategoryItem<T>[];
  onChange: (value: T) => void;
  defaultValue?: string;
  disabled?: boolean;
  hasCategory?: boolean;
  placeholder?: string;
}

const Dropdown = <T,>({ label, categories, onChange, defaultValue, disabled, hasCategory, placeholder }: Props<T>) => {
  const [isListOpen, setIsListOpen] = useState(false);
  const [selectedItem, setSelectedItem] = useState<DropDownItem<T> | null>(null);

  const findSelectedItem = useCallback(() => {
    let selected: DropDownItem<T> | null = null;

    categories?.forEach((x) => {
      x.items?.forEach((y) => {
        if (y.id === defaultValue) {
          selected = y;
        }
      });
    });
    return selected;
  }, [defaultValue, categories]);

  useEffect(() => {
    if (!categories) {
      setSelectedItem(null);
      return;
    }
    if (defaultValue) {
      const _selectedItem = findSelectedItem();
      setSelectedItem(_selectedItem);
    }
  }, [defaultValue, findSelectedItem, categories]);

  const handleChange = (item: DropDownItem<T>) => {
    setSelectedItem(item);
    setIsListOpen(!isListOpen);
    onChange(item.value);
  };

  const getCategory = (item: DropDownCategoryItem<T>) => {
    return (
      <LocationTypeCategory>
        <p>{item.description}</p>
      </LocationTypeCategory>
    );
  };

  const getItems = (items: DropDownItem<T>[]) => {
    return items?.map((item) => {
      return (
        <div onClick={() => handleChange(item)} key={item.id}>
          <DropdownMenuListItem>
            {item.image && <Symbol base64={item.image} text={item.description} />}
            <p>{item.description}</p>
          </DropdownMenuListItem>
        </div>
      );
    });
  };

  return (
    <DropdownMenuWrapper disabled={disabled ?? false}>
      <label htmlFor={label} />
      <div className="dropdown-label">{label}</div>
      <div onClick={disabled ? undefined : () => setIsListOpen(!isListOpen)}>
        <DropdownMenuHeader>
          {selectedItem && selectedItem.image && <Symbol base64={selectedItem.image} text={selectedItem.description} />}
          <p>{selectedItem?.description ?? placeholder}</p>
          <img src={isListOpen ? ExpandIcon : CollapseIcon} alt="expand-icon" />
        </DropdownMenuHeader>
      </div>
      {isListOpen && (
        <DropdownMenuList>
          {categories?.map((category) => {
            return (
              <div key={category.name}>
                {hasCategory && getCategory(category)}
                {getItems(category.items)}
              </div>
            );
          })}
        </DropdownMenuList>
      )}
    </DropdownMenuWrapper>
  );
};

export default Dropdown;
