import { Search } from "@styled-icons/heroicons-outline";
import styled from "styled-components/macro";
import { Input } from "../../complib/inputs";
import { InputProps } from "../../complib/inputs/input/Input";

export const SearchField = (props: InputProps) => (
  <Input icon={<ThinSearch />} width={"100%"} minWidth={"200px"} maxWidth={"500px"} height={"44px"} {...props} />
);

const ThinSearch = styled(Search)`
  path {
    stroke-width: 1;
  }
`;
