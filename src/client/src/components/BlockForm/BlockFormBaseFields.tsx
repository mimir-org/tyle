import {
  Button,
  Flexbox,
  FormBaseFieldsContainer,
  FormField,
  Input,
  Select,
  Text,
  Textarea,
  Token,
} from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetPurposes } from "api/purpose.queries";
import { useGetSymbols } from "api/symbol.queries";
import EngineeringSymbolSvg from "components/EngineeringSymbolSvg";
import FormSection from "components/FormSection";
import PlainLink from "components/PlainLink";
import SelectItemDialog from "components/SelectItemDialog";
import { purposeInfoItem, symbolInfoItem } from "helpers/mappers.helpers";
import { Controller, useFormContext, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Aspect } from "types/common/aspect";
import { FormMode } from "types/formMode";
import { getOptionsFromEnum } from "utils";
import { BlockFormFields } from "./BlockForm.helpers";

interface BlockFormBaseFieldsProps {
  mode?: FormMode;
  limited?: boolean;
}

/**
 * Component which contains all shared fields for variations of the block form.
 *
 * @param isFirstDraft
 * @param mode
 * @param state
 * @param limited
 * @constructor
 */
const BlockFormBaseFields = ({ mode, limited }: BlockFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, formState, setValue } = useFormContext<BlockFormFields>();
  const { errors } = formState;

  const purposeQuery = useGetPurposes();
  const purposeInfoItems = purposeQuery.data?.map((p) => purposeInfoItem(p)) ?? [];
  const chosenPurpose = useWatch({ control, name: "purpose" });

  const symbolQuery = useGetSymbols();
  const symbolInfoItems = symbolQuery.data?.map((s) => symbolInfoItem(s)) ?? [];
  const chosenSymbol = useWatch({ control, name: "symbol" });

  const aspectOptions = getOptionsFromEnum<Aspect>(Aspect);

  return (
    <FormBaseFieldsContainer>
      <Text variant={"display-small"}>{t("block.title")}</Text>

      <FormSection
        title={"Symbol"}
        action={
          <SelectItemDialog
            title={"Select symbol"}
            description={"You can select at most one symbol"}
            searchFieldText={"Search"}
            addItemsButtonText={"Add"}
            openDialogButtonText={"Open symbol selection dialog"}
            items={symbolInfoItems}
            onAdd={(ids) => {
              setValue("symbol", symbolQuery.data?.find((x) => x.id === Number(ids[0])));
            }}
            isMultiSelect={false}
          />
        }
      >
        <Input {...register("symbol")} type="hidden" />
        {chosenSymbol && <EngineeringSymbolSvg symbol={chosenSymbol} />}
      </FormSection>

      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l}>
        <FormField label={t("terminal.name")} error={errors.name}>
          <Input placeholder={t("terminal.placeholders.name")} {...register("name")} disabled={limited} />
        </FormField>

        <FormField label={t("block.description")} error={errors.description}>
          <Textarea placeholder={t("block.placeholders.description")} {...register("description")} />
        </FormField>

        <FormSection
          title={t("terminal.purpose.title")} //TODO: change name to correct section in langu file.
          action={
            !chosenPurpose && (
              <SelectItemDialog
                title={t("terminal.purpose.dialog.title")}
                description={t("terminal.purpose.dialog.description")}
                searchFieldText={t("terminal.purpose.dialog.search")}
                addItemsButtonText={t("terminal.purpose.dialog.add")}
                openDialogButtonText={t("terminal.purpose.dialog.open")}
                items={purposeInfoItems}
                onAdd={(ids) => {
                  setValue("purpose", purposeQuery.data?.find((x) => x.id === Number(ids[0])));
                }}
                isMultiSelect={false}
              />
            )
          }
        >
          <Input {...register("purpose")} type="hidden" />
          {chosenPurpose && (
            <Token
              variant={"secondary"}
              actionable
              actionIcon={<XCircle />}
              actionText={t("terminal.purpose.remove")}
              onAction={() => setValue("purpose", undefined)}
              dangerousAction
            >
              {purposeInfoItems.find((x) => x.id === chosenPurpose.id.toString())?.name}
            </Token>
          )}
        </FormSection>

        <FormField label={t("terminal.notation")} error={errors.notation}>
          <Input placeholder={t("terminal.placeholders.notation")} {...register("notation")} disabled={limited} />
        </FormField>

        <FormField label={t("block.aspect")} error={errors.aspect}>
          <Controller
            control={control}
            name={"aspect"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("block.aspect").toLowerCase() })}
                options={aspectOptions}
                getOptionLabel={(x) => x.label}
                onChange={(x) => {
                  onChange(x?.value);
                }}
                value={aspectOptions.find((x) => x.value === value)}
              />
            )}
          />
        </FormField>
      </Flexbox>

      <Flexbox justifyContent={"center"} gap={theme.mimirorg.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            {t("common.cancel")}
          </Button>
        </PlainLink>
        <Button type={"submit"}>{mode === "edit" ? t("common.edit") : t("common.submit")}</Button>
      </Flexbox>
    </FormBaseFieldsContainer>
  );
};

export default BlockFormBaseFields;
