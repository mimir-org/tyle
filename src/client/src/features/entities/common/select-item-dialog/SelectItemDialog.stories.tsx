import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockInfoItem } from "common/utils/mocks";
import { SelectItemDialog } from "features/entities/common/select-item-dialog/SelectItemDialog";

const mockData = [...Array(20)].map((_) => mockInfoItem());

export default {
  title: "Features/Entities/Common/SelectItemDialog",
  component: SelectItemDialog,
  args: {
    title: "Select item(s)",
    description: "This dialog support selecting various entities",
    searchFieldText: "Search for the item you want",
    addItemsButtonText: "Add",
    openDialogButtonText: "Open item dialog",
    items: mockData,
    onAdd: () => alert("[STORYBOOK] SelectItemDialog.Add"),
  },
} as ComponentMeta<typeof SelectItemDialog>;

const Template: ComponentStory<typeof SelectItemDialog> = (args) => <SelectItemDialog {...args} />;

export const Default = Template.bind({});
