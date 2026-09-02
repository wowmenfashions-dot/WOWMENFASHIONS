USE wowmenfashions;
GO

DECLARE @TermsContent NVARCHAR(MAX) = N'
<p class="mb-6 text-muted">
    <em>Last Updated: September 2026</em><br/>
    Please read these Terms and Conditions ("Terms", "Terms and Conditions") carefully before using the WOWMENFASHIONS website operated by WOWMENFASHIONS ("us", "we", or "our").
</p>

<h6 class="fw-bold mb-3 mt-6">1. Financial Liability & Payment Information</h6>
<ul class="mb-6" style="list-style-type: disc; margin-left: 20px;">
    <li><strong>No Credit Card Storage:</strong> We do NOT store, process, or keep any credit card details or sensitive payment information on our servers. All transactions are securely processed through our third-party payment gateway (Razorpay).</li>
    <li><strong>No Financial Liability:</strong> In no event shall WOWMENFASHIONS, its owners, directors, employees, or affiliates be liable for any direct, indirect, incidental, special, or consequential financial losses or damages arising out of the use of our payment gateway, fraudulent transactions, or banking errors.</li>
</ul>

<h6 class="fw-bold mb-3 mt-6">2. Privacy & Data Sharing</h6>
<ul class="mb-6" style="list-style-type: disc; margin-left: 20px;">
    <li><strong>No Marketing Sales:</strong> We respect your privacy. We do not sell, rent, trade, or share your personal details, contact information, or purchasing history with any external marketing agencies, advertisers, or third parties.</li>
    <li><strong>Data Usage:</strong> Your personal information is strictly used for the fulfillment of orders, customer support, and necessary communication regarding your purchases.</li>
</ul>

<h6 class="fw-bold mb-3 mt-6">3. Limitation of Liability</h6>
<ul class="mb-6" style="list-style-type: disc; margin-left: 20px;">
    <li><strong>General Disclaimer:</strong> The owner of this application and business shall not be held responsible for any legal, financial, or personal issues arising from the use of this website, delayed shipments, or product misuse.</li>
    <li><strong>"As Is" Service:</strong> Our service and products are provided on an "AS IS" and "AS AVAILABLE" basis without any representation or warranties, express or implied.</li>
</ul>

<h6 class="fw-bold mb-3 mt-6">4. Product Descriptions</h6>
<p class="mb-6">
    We attempt to be as accurate as possible. However, we do not warrant that product descriptions, colors, or other content of this site are perfectly accurate, complete, reliable, current, or error-free.
</p>

<hr class="my-8" />

<p class="text-muted d-block text-center" style="font-size: 0.75rem;">
    By accessing or using our service, you agree to be bound by these Terms. If you disagree with any part of the terms, then you may not access the service. <br/>
    For questions regarding these terms, please contact us via our Contact Us page.
</p>
';

IF EXISTS (SELECT 1 FROM PageContents WHERE PageName = 'Terms')
BEGIN
    UPDATE PageContents SET ContentHtml = @TermsContent WHERE PageName = 'Terms';
END
ELSE
BEGIN
    INSERT INTO PageContents (PageName, ContentHtml, LastUpdated) VALUES ('Terms', @TermsContent, GETDATE());
END
GO
