import { Flexbox } from "@mimirorg/component-library";
import { AttributeGroupLibCm, AttributeLibCm } from "@mimirorg/typelibrary-types";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import { UseFormRegisterReturn } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import { AttributeGroupView } from "types/attributes/attributeGroupView";
import { ValueObject } from "types/valueObject";
import { onAddAttributeGroup } from "./FormAttributeGroups.helpers";

export interface FormAttributeGroupsProps {
  fields: ValueObject<string>[];
  append: (item: ValueObject<string>) => void;
  remove: (index: number) => void;
  register: (index: number) => UseFormRegisterReturn;
  preprocess?: (attributeGroups?: AttributeGroupView[]) => AttributeGroupLibCm[];
  canAddAttributeGroups?: boolean;
  canRemoveAttributeGroups?: boolean;
  limitedAttributeGroups?: AttributeLibCm[];
}

/**
 * Reusable form section for adding attributeGroups
 *
 * @param fields
 * @param append
 * @param remove
 * @param register
 * @param preprocess pass a function to alter the attributeGroup data before it is shown to the user
 * @param canAddAttributeGroups controls if the add action is shown
 * @param canRemoveAttributeGroups controls if the remove action is shown
 * @param limitedAttributeGroups attributeGroups that cannot be removed, even if removing attributeGroups is allowed
 * @constructor
 */
const FormAttributeGroups = ({ append, canAddAttributeGroups = true }: FormAttributeGroupsProps) => {
  const theme = useTheme();

  //const attributeQuery = useGetAttributeGroups();

  //Add get attributes also for each group? If above is not sufficent ie name
  //const attributeGroups = preprocess ? preprocess(attributeQuery.data) : attributeQuery.data ?? [];
  //const [available, selected] = resolveSelectedAndAvailableAttributeGroups(fields, attributeGroups);

  return (
    <FormSection
      title="Attribute Group"
      action={
        canAddAttributeGroups && (
          <SelectItemDialog
            title="Select attribute groups(s)"
            description="The attribute groups listed below can be used across multiple entities."
            searchFieldText="Search"
            addItemsButtonText="Add"
            openDialogButtonText="Open attribute group(s) selection dialog"
            items={[]}
            onAdd={(ids) => onAddAttributeGroup(ids, [], append)}
          />
        )
      }
    >
      <Flexbox flexWrap={"wrap"} gap={theme.mimirorg.spacing.xl}>
        {/*fields.map((field, index) => {
          const attributeGroup = selected.find((x) => x.id === field.value);
          return (
            attributeGroup && (
              <Token
                variant={"primary"}
                key={attributeGroup.id}
                {...register(index)}
                actionable={
                  canRemoveAttributeGroups && !limitedAttributeGroups.map((x) => x.id).includes(attributeGroup.id ?? "")
                }
                actionIcon={<XCircle />}
                actionText={t("attributes.remove")}
                onAction={() => remove(index)}
                dangerousAction
              >
                {attributeGroup.name}
              </Token>
            )
          );
        })*/}
      </Flexbox>
    </FormSection>
  );
};

export default FormAttributeGroups;
