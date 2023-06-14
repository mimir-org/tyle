import { Meta, StoryFn } from "@storybook/react";
import { Input } from "complib/inputs";
import { FieldsCard } from "features/entities/common/fields-card/FieldsCard";

export default {
  title: "Features/Entities/Common/FieldsCard",
  component: FieldsCard,
} as Meta<typeof FieldsCard>;

const Template: StoryFn<typeof FieldsCard> = (args) => <FieldsCard {...args} />;

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
