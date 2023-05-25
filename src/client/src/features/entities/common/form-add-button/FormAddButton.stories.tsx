import { Meta, StoryFn } from "@storybook/react";
import { FormAddButton } from "features/entities/common/form-add-button/FormAddButton";

export default {
  title: "Features/Entities/Common/FormAddButton",
  component: FormAddButton,
} as Meta<typeof FormAddButton>;

const Template: StoryFn<typeof FormAddButton> = (args) => <FormAddButton {...args} />;

export const Default = Template.bind({});
Default.args = {
  buttonText: "Add entity",
  onClick: () => alert("[STORYBOOK] FormAddButton.onClick"),
};
