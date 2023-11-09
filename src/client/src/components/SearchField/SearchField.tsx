import { Input, InputProps } from "@mimirorg/component-library";
import SearchFieldIcon from "./SearchField.styled";

const SearchField = (props: InputProps) => (
  <Input icon={<SearchFieldIcon />} width={"100%"} minWidth={"200px"} maxWidth={"500px"} height={"44px"} {...props} />
);

export default SearchField;
