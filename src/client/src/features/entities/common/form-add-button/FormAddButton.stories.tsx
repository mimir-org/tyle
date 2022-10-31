import { ComponentMeta, ComponentStory } from "@storybook/react";
import { FormAddButton } from "features/entities/common/form-add-button/FormAddButton";

export default {
  title: "Entities/Common/FormAddButton",
  component: FormAddButton,
} as ComponentMeta<typeof FormAddButton>;

const Template: ComponentStory<typeof FormAddButton> = (args) => <FormAddButton {...args} />;

export const Default = Template.bind({});
Default.args = {
  buttonText: "Add entity",
  onClick: () => alert("[STORYBOOK] FormAddButton.onClick"),
};
