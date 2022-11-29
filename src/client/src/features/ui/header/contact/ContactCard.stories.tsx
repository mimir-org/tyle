import { ComponentStory } from "@storybook/react";
import { ContactCard } from "features/ui/header/contact/ContactCard";

export default {
  title: "Features/UI/Header/ContactCard",
  component: ContactCard,
};

const Template: ComponentStory<typeof ContactCard> = (args) => <ContactCard {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Threepwood",
  email: "guybrush.threepwood@island.com",
};
