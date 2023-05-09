import { DatumItem } from "../../../common/types/datumItem";
import styled from "styled-components/macro";
import { Text } from "../../../complib/text";
import Badge from "../../ui/badges/Badge";

const StyledDiv = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
  max-width: 40rem;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  height: fit-content;
`;

interface DatumPreviewProps {
  datum: Partial<DatumItem>;
}

enum quantityDatumTypeString {
  "Specified scope",
  "Specified provenance",
  "Specified range",
  "Specified regularity",
}
export default function DatumPreview({ datum }: DatumPreviewProps) {
  return (
    <StyledDiv>
      <Text fontSize={"24px"}>{datum.name}</Text>
      <Text>{datum.description}</Text>
      {datum.quantityType != undefined ? (
        <Badge variant={"info"}>{quantityDatumTypeString[datum.quantityType]}</Badge>
      ) : null}
    </StyledDiv>
  );
}
