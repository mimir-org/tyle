import { Input } from "complib/inputs";
import { InputProps } from "complib/inputs/input/Input";
import { SearchFieldIcon } from "features/common/search-field/SearchField.styled";

export const SearchField = (props: InputProps) => (
  <Input icon={<SearchFieldIcon />} width={"100%"} minWidth={"200px"} maxWidth={"500px"} height={"44px"} {...props} />
);
