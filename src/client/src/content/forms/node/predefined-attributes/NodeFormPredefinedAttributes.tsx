import { Aspect } from "@mimirorg/typelibrary-types";
import { Control, Controller, UseFormRegister } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import { TextResources } from "../../../../assets/text";
import { FormField } from "../../../../complib/form";
import { Input, Select } from "../../../../complib/inputs";
import { Box, Grid } from "../../../../complib/layouts";
import { useGetAttributesPredefined } from "../../../../data/queries/tyle/queriesAttribute";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormSection } from "../NodeFormSection";
import { preparePredefinedAttributes } from "./NodeFormPredefinedAttributes.helpers";

export interface NodeFormPredefinedAttributesProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
  aspects?: Aspect[];
}

export const NodeFormPredefinedAttributes = ({ control, register, aspects }: NodeFormPredefinedAttributesProps) => {
  const theme = useTheme();
  const predefinedAttributesQuery = useGetAttributesPredefined();
  const predefinedAttributes = preparePredefinedAttributes(predefinedAttributesQuery.data, aspects);

  return (
    <NodeFormSection title={TextResources.PREDEFINED_ATTRIBUTE_TITLE}>
      <Grid gridTemplateColumns={"repeat(auto-fill, 300px)"} gap={theme.tyle.spacing.xl}>
        {predefinedAttributes.map((x, index) => {
          return (
            <Box key={x.key}>
              <Input {...register(`selectedAttributePredefined.${index}.key`)} type={"hidden"} value={x.key} />
              <FormField label={x.key}>
                <Controller
                  control={control}
                  name={`selectedAttributePredefined.${index}.values`}
                  render={({ field: { ref, onChange, ...rest } }) => (
                    <Select
                      {...rest}
                      selectRef={ref}
                      placeholder={TextResources.PREDEFINED_ATTRIBUTE_SELECT}
                      options={x.valueStringList.map((y) => ({ value: y }))}
                      getOptionLabel={(y) => y.value}
                      isLoading={predefinedAttributesQuery.isLoading}
                      isMulti={x.isMultiSelect}
                      onChange={(val) => {
                        if (!Array.isArray(val)) onChange([val]);
                        else onChange(val);
                      }}
                    />
                  )}
                />
              </FormField>
            </Box>
          );
        })}
      </Grid>
    </NodeFormSection>
  );
};
