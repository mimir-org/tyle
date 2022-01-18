import { ListType, RadioButtonContainer } from "../../inputs/RadioButtonContainer";
import { Rds } from "../../../../../models";
import { ListCategoryElement, ListElementCategoryWrapper, RdsListElement } from "../../../styled";
import { OnPropertyChangeFunction } from "../../../types";

interface Props {
  category: string;
  rds: Rds[];
  defaultValue: string;
  onChange: OnPropertyChangeFunction;
}

export const RDSElement = ({ category, rds, defaultValue, onChange }: Props) => (
  <ListElementCategoryWrapper>
    <ListCategoryElement>
      <p>{category}</p>
    </ListCategoryElement>
    {rds.map((element) => (
      <RdsListElement key={element.id} isSelected={element.id === defaultValue}>
        <RadioButtonContainer
          id={element.id}
          label={element.code + " - " + element.name}
          listType={ListType.Rds}
          onChange={(key, data) => onChange(key, data)}
          defaultValue={defaultValue}
        />
      </RdsListElement>
    ))}
  </ListElementCategoryWrapper>
);

export default RDSElement;
