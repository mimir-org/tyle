import {
  Button,
  Flexbox,
  FormBaseFieldsContainer,
  FormField,
  Input,
  Text,
  Textarea,
} from "@mimirorg/component-library";
import PlainLink from "components/PlainLink";
import { useFormContext } from "react-hook-form";
import { useTheme } from "styled-components";
import { FormMode } from "types/formMode";
import { FormAttributeGroupLib } from "./formAttributeGroupLib";

interface AttributeGroupFormBaseFieldsProps {
  mode?: FormMode;
  limited?: boolean;
}

/**
 * Component which contains all simple value fields of the AttributeGroup form.
 *
 * @param mode
 * @param limited
 * @constructor
 */
const AttributeGroupFormBaseFields = ({ mode, limited }: AttributeGroupFormBaseFieldsProps) => {
  const theme = useTheme();
  const { register, formState } = useFormContext<FormAttributeGroupLib>();
  const { errors } = formState;

  return (
    <FormBaseFieldsContainer>
      <Text variant={"display-small"}>Attribute Group</Text>
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l}>
        <FormField label="Name" error={errors.name}>
          <Input placeholder="Name" {...register("name")} disabled={limited} />
        </FormField>

        <FormField label="Description" error={errors.description}>
          <Textarea
            placeholder="Additional information about this attribute group can be supplied here."
            {...register("description")}
          />
        </FormField>
      </Flexbox>

      <Flexbox justifyContent={"center"} gap={theme.mimirorg.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            Cancel
          </Button>
        </PlainLink>
        <Button type={"submit"}>{mode === "edit" ? "Apply changes" : "Submit"}</Button>
      </Flexbox>
    </FormBaseFieldsContainer>
  );
};

export default AttributeGroupFormBaseFields;
