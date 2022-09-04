import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockInfoItem } from "../../../utils/mocks/mockInfoItem";
import { SelectItemDialog } from "./SelectItemDialog";

const mockData = [...Array(20)].map((_) => mockInfoItem());

export default {
  title: "Content/Forms/Common/SelectItemDialog",
  component: SelectItemDialog,
  args: {
    attributes: mockData,
    onAdd: () => alert("[STORYBOOK] SelectItemDialog.Add"),
  },
} as ComponentMeta<typeof SelectItemDialog>;

const Template: ComponentStory<typeof SelectItemDialog> = (args) => <SelectItemDialog {...args} />;

export const Default = Template.bind({});
