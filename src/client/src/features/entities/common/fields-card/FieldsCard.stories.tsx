import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Input } from "../../../../complib/inputs";
import { FieldsCard } from "./FieldsCard";

export default {
  title: "Entities/Common/FieldsCard",
  component: FieldsCard,
} as ComponentMeta<typeof FieldsCard>;

const Template: ComponentStory<typeof FieldsCard> = (args) => <FieldsCard {...args} />;

export const Default = Template.bind({});
Default.args = {
  index: 1,
  removeText: "Remove value",
  onRemove: () => alert("[STORYBOOK] FormSection.Remove"),
  children: (
    <>
      <Input placeholder={"Some value here"} />
      <Input placeholder={"Another value here"} />
    </>
  ),
};
