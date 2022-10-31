import { SearchFieldIcon } from "common/components/search-field/SearchField.styled";
import { Input } from "complib/inputs";
import { InputProps } from "complib/inputs/input/Input";

export const SearchField = (props: InputProps) => (
  <Input icon={<SearchFieldIcon />} width={"100%"} minWidth={"200px"} maxWidth={"500px"} height={"44px"} {...props} />
);
