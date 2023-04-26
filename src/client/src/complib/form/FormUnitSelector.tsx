import { UnitLibCm } from "@mimirorg/typelibrary-types";
import styled from "styled-components";
import { Heading, Text } from "../text";

interface FormUnitSelectorProps {
  units: UnitLibCm[];
}

const StyledUnit = styled.div`
  display: flex;
  flex-direction: column;
  gap: 8px;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
  margin-bottom: 8px;
  max-height: 200px;
  justify-content: center;
  align-content: center;
`;

const UnitGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 8px;
`;

export default function FormUnitSelector({ units }: FormUnitSelectorProps) {
  return (
    <UnitGrid>
      {units.map((unit) => {
        return (
          <StyledUnit key={unit.id}>
            <Heading fontSize={"24px"}>{unit.name}</Heading>
            <Text fontSize={"16px"} color={"gray"}>
              {unit.symbol}
            </Text>
            <Text>{unit.description}</Text>
          </StyledUnit>
        );
      })}
    </UnitGrid>
  );
}
