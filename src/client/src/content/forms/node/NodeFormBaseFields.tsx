import { Aspect } from "@mimirorg/typelibrary-types";
import {
  Control,
  Controller,
  UseFormRegister,
  UseFormReset,
  UseFormResetField,
  UseFormSetValue,
} from "react-hook-form";
import { useTheme } from "styled-components/macro";
import textResources from "../../../assets/text/TextResources";
import { Button } from "../../../complib/buttons";
import { Popover } from "../../../complib/data-display";
import { FormField } from "../../../complib/form";
import { Input, Select, Textarea } from "../../../complib/inputs";
import { Box, Flexbox } from "../../../complib/layouts";
import { Icon } from "../../../complib/media";
import { Text } from "../../../complib/text";
import { ConditionalWrapper } from "../../../complib/utils";
import { useGetCompanies } from "../../../data/queries/auth/queriesCompany";
import { useGetPurposes } from "../../../data/queries/tyle/queriesPurpose";
import { useGetRds } from "../../../data/queries/tyle/queriesRds";
import { useGetSymbols } from "../../../data/queries/tyle/queriesSymbol";
import { getValueLabelObjectsFromEnum } from "../../../utils/getValueLabelObjectsFromEnum";
import { PlainLink } from "../../utils/PlainLink";
import { FormNodeLib } from "../types/formNodeLib";
import { resetSubform, usePrefilledNodeData } from "./NodeForm.helpers";
import { NodeFormBaseFieldsContainer } from "./NodeFormBaseFields.styled";
import { NodeFormPreview } from "./NodeFormPreview";

interface NodeFormBaseFieldsProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
  reset: UseFormReset<FormNodeLib>;
  resetField: UseFormResetField<FormNodeLib>;
  setValue: UseFormSetValue<FormNodeLib>;
}

/**
 * Component which contains all shared fields for variations of the node form.
 *
 * @param control
 * @param register
 * @param reset
 * @param resetField
 * @param setValue
 * @constructor
 */
export const NodeFormBaseFields = ({ control, register, reset, resetField, setValue }: NodeFormBaseFieldsProps) => {
  const theme = useTheme();

  const rdsQuery = useGetRds();
  const symbolQuery = useGetSymbols();
  const purposeQuery = useGetPurposes();
  const companyQuery = useGetCompanies();
  const aspectOptions = getValueLabelObjectsFromEnum<Aspect>(Aspect);

  const hasPrefilledData = usePrefilledNodeData(reset);

  return (
    <NodeFormBaseFieldsContainer>
      <NodeFormPreview control={control} />

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={textResources.FORMS_NODE_NAME}>
          <Input placeholder={textResources.FORMS_NODE_NAME_PLACEHOLDER} {...register("name")} />
        </FormField>
        <FormField label={textResources.FORMS_NODE_PURPOSE}>
          <Controller
            control={control}
            name={"purposeName"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={textResources.FORMS_NODE_PURPOSE_PLACEHOLDER}
                options={purposeQuery.data}
                isLoading={purposeQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={purposeQuery.data?.find((x) => x.name === value)}
              />
            )}
          />
        </FormField>
        <FormField label={textResources.FORMS_NODE_ASPECT}>
          <Controller
            control={control}
            name={"aspect"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <ConditionalWrapper
                condition={hasPrefilledData}
                wrapper={(c) => (
                  <Popover align={"start"} maxWidth={"225px"} content={textResources.FORMS_NODE_ASPECT_DISABLED}>
                    <Box borderRadius={theme.tyle.border.radius.medium} tabIndex={0}>
                      {c}
                    </Box>
                  </Popover>
                )}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={textResources.FORMS_NODE_ASPECT_PLACEHOLDER}
                  options={aspectOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => {
                    resetSubform(resetField);
                    onChange(x?.value);
                  }}
                  value={aspectOptions.find((x) => x.value === value)}
                  isDisabled={hasPrefilledData}
                />
              </ConditionalWrapper>
            )}
          />
        </FormField>
        <FormField label={textResources.FORMS_NODE_SYMBOL}>
          <Controller
            control={control}
            name={"symbol"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={textResources.FORMS_NODE_SYMBOL_PLACEHOLDER}
                options={symbolQuery.data}
                isLoading={symbolQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.data}
                onChange={(x) => onChange(x?.data)}
                value={symbolQuery.data?.find((x) => x.data === value)}
                formatOptionLabel={(x) => (
                  <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
                    <Icon src={x.data} />
                    <Text>{x.name}</Text>
                  </Flexbox>
                )}
              />
            )}
          />
        </FormField>
        <Input type={"hidden"} {...register("rdsCode")} />
        <FormField label={textResources.FORMS_NODE_RDS}>
          <Controller
            control={control}
            name={"rdsName"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={textResources.FORMS_NODE_RDS_PLACEHOLDER}
                options={rdsQuery.data}
                isLoading={rdsQuery.isLoading}
                getOptionLabel={(x) => `${x.id} - ${x.name}`}
                getOptionValue={(x) => x.id}
                value={rdsQuery.data?.find((x) => x.name === value)}
                onChange={(rds) => {
                  if (rds) {
                    setValue("rdsCode", rds.id, { shouldDirty: true });
                    onChange(rds.name);
                  }
                }}
              />
            )}
          />
        </FormField>
        <FormField label={textResources.FORMS_NODE_OWNER}>
          <Controller
            control={control}
            name={"companyId"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={textResources.FORMS_NODE_OWNER_PLACEHOLDER}
                options={companyQuery.data}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.id.toString()}
                onChange={(x) => {
                  onChange(x?.id);
                }}
                value={companyQuery.data?.find((x) => x.id === value)}
              />
            )}
          />
        </FormField>
        <FormField label={textResources.FORMS_NODE_DESCRIPTION}>
          <Textarea placeholder={textResources.FORMS_NODE_DESCRIPTION_PLACEHOLDER} {...register("description")} />
        </FormField>
      </Flexbox>

      <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            {textResources.FORMS_CANCEL}
          </Button>
        </PlainLink>
        <Button type={"submit"}>{textResources.FORMS_SUBMIT}</Button>
      </Flexbox>
    </NodeFormBaseFieldsContainer>
  );
};
