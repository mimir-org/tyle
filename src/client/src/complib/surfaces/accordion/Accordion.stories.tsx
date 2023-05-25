import { Meta, StoryFn } from "@storybook/react";
import { Button } from "complib/buttons";
import { Accordion } from "complib/surfaces/accordion/Accordion";
import { AccordionContent } from "complib/surfaces/accordion/components/AccordionContent";
import { AccordionItem } from "complib/surfaces/accordion/components/AccordionItem";
import { AccordionTrigger } from "complib/surfaces/accordion/components/AccordionTrigger";

export default {
  title: "Surfaces/Accordion",
  component: Accordion,
  subcomponents: { AccordionItem, AccordionTrigger, AccordionContent },
} as Meta<typeof Accordion>;

const Template: StoryFn<typeof Accordion> = (args) => <Accordion {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: (
    <AccordionItem value={"item01"}>
      <AccordionTrigger>Accordion label</AccordionTrigger>
      <AccordionContent>Example with plain text as inner content of accordion</AccordionContent>
    </AccordionItem>
  ),
};

export const WithMultipleItems = Template.bind({});
WithMultipleItems.args = {
  children: (
    <>
      <AccordionItem value={"item01"}>
        <AccordionTrigger>Accordion label 1</AccordionTrigger>
        <AccordionContent>Example with plain text as inner content of accordion</AccordionContent>
      </AccordionItem>
      <AccordionItem value={"item02"}>
        <AccordionTrigger>Accordion label 2</AccordionTrigger>
        <AccordionContent>Example with plain text as inner content of accordion</AccordionContent>
      </AccordionItem>
      <AccordionItem value={"item03"}>
        <AccordionTrigger>Accordion label 3</AccordionTrigger>
        <AccordionContent>Example with plain text as inner content of accordion</AccordionContent>
      </AccordionItem>
    </>
  ),
};

export const WithMultipleOpenItems = Template.bind({});
WithMultipleOpenItems.args = {
  ...WithMultipleItems.args,
  type: "multiple",
};

export const WithCustomContent = Template.bind({});
WithCustomContent.args = {
  children: (
    <AccordionItem value={"item01"}>
      <AccordionTrigger>Accordion label</AccordionTrigger>
      <AccordionContent>
        <Button>A wonderful button</Button>
      </AccordionContent>
    </AccordionItem>
  ),
};
