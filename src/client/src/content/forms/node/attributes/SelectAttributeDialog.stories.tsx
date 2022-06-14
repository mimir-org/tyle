import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockAttributeItem } from "../../../../utils/mocks/mockAttributeItem";
import { SelectAttributeDialog } from "./SelectAttributeDialog";

const mockData = [...Array(20)].map((_) => mockAttributeItem());

export default {
  title: "Content/Forms/Node/SelectAttributeDialog",
  component: SelectAttributeDialog,
  args: {
    attributes: mockData,
    onAdd: () => alert("[STORYBOOK] SelectAttributeDialog.Add"),
  },
} as ComponentMeta<typeof SelectAttributeDialog>;

const Template: ComponentStory<typeof SelectAttributeDialog> = (args) => <SelectAttributeDialog {...args} />;

export const Default = Template.bind({});
