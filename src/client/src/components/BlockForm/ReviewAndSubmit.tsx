import { Button, Flexbox, PlainLink } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import { FormMode } from "types/formMode";
import { BlockFormFields } from "./BlockForm.helpers";

interface ReviewAndSubmitProps {
  blockFormFields: BlockFormFields;
  mode?: FormMode;
}

const ReviewAndSubmit = ({ blockFormFields, mode }: ReviewAndSubmitProps) => {
  const theme = useTheme();

  return (
    <Flexbox gap={theme.mimirorg.spacing.xl} flexDirection="column">
      <p>
        {
          /* TODO: Add an actual block preview */
          JSON.stringify(blockFormFields)
        }
      </p>

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

export default ReviewAndSubmit;
