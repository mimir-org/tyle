import { Button, Flexbox, PlainLink, Table, Tbody, Td, Tr } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import { Aspect } from "types/common/aspect";
import { FormMode } from "types/formMode";
import { Direction } from "types/terminals/direction";
import { BlockFormFields } from "./BlockForm.helpers";

interface ReviewAndSubmitProps {
  blockFormFields: BlockFormFields;
  mode?: FormMode;
}

const ReviewAndSubmitStep = ({ blockFormFields, mode }: ReviewAndSubmitProps) => {
  const theme = useTheme();

  return (
    <Flexbox gap={theme.mimirorg.spacing.xl} flexDirection="column">
      <Table>
        <Tbody>
          <Tr>
            <Td>Name</Td>
            <Td>{blockFormFields.name}</Td>
          </Tr>
          <Tr>
            <Td>Notation</Td>
            <Td>{blockFormFields.notation}</Td>
          </Tr>
          <Tr>
            <Td>Aspect</Td>
            <Td>{blockFormFields.aspect ? Aspect[blockFormFields.aspect] : "undefined"}</Td>
          </Tr>
          <Tr>
            <Td>Purpose</Td>
            <Td>{blockFormFields.purpose ? blockFormFields.purpose.name : "undefined"}</Td>
          </Tr>
          <Tr>
            <Td>Description</Td>
            <Td>{blockFormFields.description}</Td>
          </Tr>
          <Tr>
            <Td>Classifiers</Td>
            <Td>
              {blockFormFields.classifiers.length > 0
                ? blockFormFields.classifiers.map((classifier) => classifier.name).join(", ")
                : "none"}
            </Td>
          </Tr>
          <Tr>
            <Td>Attributes</Td>
            <Td>
              {blockFormFields.attributes.length > 0
                ? blockFormFields.attributes
                    .map(
                      (attribute) =>
                        `${attribute.attribute.name} (min: ${attribute.minCount}${
                          attribute.maxCount ? `, max: ${attribute.maxCount}` : ""
                        })`,
                    )
                    .join(", ")
                : "none"}
            </Td>
          </Tr>
          <Tr>
            <Td>Terminals</Td>
            <Td>
              {blockFormFields.terminals.length > 0
                ? blockFormFields.terminals
                    .map(
                      (terminal) =>
                        `${terminal.terminalName} (${Direction[terminal.direction]}, min: ${terminal.minCount}${
                          terminal.maxCount ? `, max: ${terminal.maxCount}` : ""
                        })`,
                    )
                    .join(", ")
                : "none"}
            </Td>
          </Tr>
          <Tr>
            <Td>Symbol</Td>
            <Td>{blockFormFields.symbol ? blockFormFields.symbol.label : "none"}</Td>
          </Tr>
        </Tbody>
      </Table>

      {!blockFormFields.name && <p style={{ color: "red" }}>Required field name is missing</p>}

      <Flexbox gap={theme.mimirorg.spacing.xl}>
        <Button type="submit" disabled={!blockFormFields.name}>
          {mode === "edit" ? "Save changes" : "Create new type"}
        </Button>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            Cancel
          </Button>
        </PlainLink>
      </Flexbox>
    </Flexbox>
  );
};

export default ReviewAndSubmitStep;
